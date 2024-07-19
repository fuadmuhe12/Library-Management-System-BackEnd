using Hangfire;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Enums;
using Library_Management_System_BackEnd.Helper.Mail;
using Library_Management_System_BackEnd.Helper.Query;
using Library_Management_System_BackEnd.Helper.Response;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Serilog;

namespace Library_Management_System_BackEnd.Repository
{
    public class BorrowingRecordRepository(
        LibraryContext context,
        IBookRepository bookRepo,
        IReservationRepository reservationRepo,
        UserManager<User> userManager,
        IEmailService emailService,
        INotificationRepository notificationRepo,
        IFineRepository fineRepo,
        IConfiguration config
    ) : IBorrowingRecordRepository
    {
        public LibraryContext _context = context;
        public IBookRepository _bookRepo = bookRepo;
        public IReservationRepository _reservationRepo = reservationRepo;
        public UserManager<User> _userManager = userManager;
        public IEmailService _emailService = emailService;
        public INotificationRepository _notificationRepo = notificationRepo;
        public IFineRepository _fineRepo = fineRepo;
        private readonly IConfiguration _config = config;

        public async Task<BorrowingRecordResponce> CreateBorrowingRecord(BorrowingRecord record)
        {
            try
            {
                await _context.BorrowingRecords.AddAsync(record);
                await _bookRepo.UpdateBookStatus(record.BookId, BookStatus.CheckedOut);
                return new BorrowingRecordResponce { IsSuccess = true };
            }
            catch (System.Exception er)
            {
                return new BorrowingRecordResponce { IsSuccess = false, ErrorMessage = er.Message };
            }
        }

        public async Task<bool> IsUserBorrowedBook(string userId, int bookId)
        {
            return await _context.BorrowingRecords.AnyAsync(record =>
                record.UserId == userId && record.BookId == bookId && record.IsReturned == false
            );
        }

        public async Task ReturnBook(string userId, int bookId)
        {
            var firstResevation = await _reservationRepo.GetCurrentReservation(bookId);
            var record = await _context
                .BorrowingRecords.Include(rec => rec.Book)
                .Include(rec => rec.User)
                .FirstOrDefaultAsync(record =>
                    record.UserId == userId && record.BookId == bookId && record.IsReturned == false
                );
            record!.ReturnDate = DateTime.Now;
            record.IsReturned = true;

            if (record.DueDate < record.ReturnDate)
            {
                await FineUser(userId, record);
            }

            if (firstResevation != null)
            {
                await RemindUser(userId, bookId, firstResevation);
            }
            else
            {
                await _bookRepo.UpdateBookStatus(bookId, BookStatus.Available);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemindUser(string userId, int bookId, Reservation? firstResevation)
        {
            Log.Information("Reminding user with id {userId} and book id {bookId}", userId, bookId);
            await _bookRepo.UpdateBookStatus(bookId, BookStatus.Reserved);

            var notification = await _notificationRepo.CreateNotification(
                userId,
                "Book Availability",
                $"Dear {firstResevation!.User!.FirstName},\n\nWe are pleased to inform you that the book \"{firstResevation.Book!.Title}\" is now available for pickup. Please collect it within the next {_config["AppConfig:ResevationExipireHour"]} hours to ensure it remains reserved for you. If you are unable to do so, the book will be made available to the next person in line.\n\nThank you for using our library services."
            );
            var mailRequest = notification.MapToMailRequest(firstResevation.User!.Email!);
            await _emailService.SendEmailAsync(mailRequest);

            BackgroundJob.Schedule(
                () => RemoveCurrentReservation(firstResevation.ReservationId, bookId),
                TimeSpan.FromDays(int.Parse(_config["AppConfig:ResevationExipireHour"]!))
            );
        }

        public async Task FineUser(string userId, BorrowingRecord? record)
        {
            var diff = (record!.ReturnDate! - record.DueDate).Value.Days;
            decimal fineAmaont = diff * 0.5m;
            var fine = record.MapToFine(fineAmaont);

            var resultFine = await _fineRepo.CreateFine(fine);

            var notification = await _notificationRepo.CreateNotification(
                userId,
                "Fine Notification",
                $"Dear {record.User!.FirstName},\n\nWe regret to inform you that you have returned the book \"{record.Book!.Title}\" late. As a result, you have been fined ${fineAmaont}. Please pay the fine as soon as possible to avoid any further penalties.\n\nThank you for using our library services."
            );

            var mailRequest = notification.MapToMailRequest(record.User!.Email!);
            await _emailService.SendEmailAsync(mailRequest);
        }

        public async Task RemoveCurrentReservation(int reservationId, int bookId)
        {
            Log.Information("Removing reservation with id {reservationId}", reservationId);
            var Oldreservation = await _reservationRepo.GetReservationById(reservationId);
            if (Oldreservation != null && Oldreservation.Status == ReservationStatus.Pending)
            {
                {
                    await _reservationRepo.UpdateReservationStatus(
                        reservationId,
                        ReservationStatus.Failed
                    );
                    var curReservation = await _reservationRepo.GetCurrentReservation(bookId);
                    if (curReservation != null)
                    {
                        await RemindUser(curReservation.UserId, bookId, curReservation);
                    }
                    else
                    {
                        await _bookRepo.UpdateBookStatus(bookId, BookStatus.Available);
                    }
                }
            }
        }

        public async Task<List<BorrowingRecord>> GetAllBorrowingRecord(DateTime Deadline)
        {
            return await _context
                .BorrowingRecords.Include(record => record.Book)
                .Include(record => record.Book!.Author)
                .Include(record => record.User)
                .Where(record =>
                    record.DueDate <= Deadline
                    && record.IsReturned == false
                    && DateTime.Now < record.DueDate
                )
                .ToListAsync();
        }

        public async Task<List<BorrowingRecord>> GetAllUserBorrowRecord(
            string userId,
            BorrowingRecordQuery query
        )
        {
            var borrows = _context
                .BorrowingRecords.Include(reser => reser.Book)
                .ThenInclude(book => book!.Author)
                .Include(reser => reser.Book)
                .ThenInclude(book => book!.Category)
                .Include(reser => reser.Book)
                .ThenInclude(book => book!.BookTags)!
                .ThenInclude(bookTag => bookTag.Tag)
                .Include(reser => reser.User)
                .Where(borrow => borrow.UserId == userId)
                .AsQueryable();

            if (query.Search && query.SearchValue != null)
            {
                switch (query.SearchBy)
                {
                    case SearchBy.Title:
                        borrows = borrows.Where(reser =>
                            reser.Book!.Title.Contains(query.SearchValue!)
                        );
                        break;
                    case SearchBy.Author:
                        borrows = borrows.Where(reser =>
                            reser.Book!.Author!.AuthorName.Contains(query.SearchValue!)
                        );
                        break;
                    case SearchBy.ISBN:
                        borrows = borrows.Where(reser =>
                            reser.Book!.ISBN.Contains(query.SearchValue!)
                        );
                        break;
                    default:
                        break;
                }
            }

            if (query.IsReturned != null)
            {
                borrows = borrows.Where(borrow => borrow.IsReturned == query.IsReturned);
            }

            if (query.FilterByTags != null)
            {
                borrows = borrows.Where(reser =>
                    reser.Book!.BookTags!.Any(bookTag =>
                        query
                            .FilterByTags.Select(tags => tags.ToString())
                            .Contains(bookTag.Tag!.TagName)
                    )
                );
            }
            if (query.SortBy != null)
            {
                if (query.SortBy == SortByReservation.BookName)
                {
                    borrows = query.IsDecensing
                        ? borrows.OrderByDescending(reser => reser.Book!.Title)
                        : borrows.OrderBy(reser => reser.Book!.Title);
                }
                if (query.SortBy == SortByReservation.AuthorName)
                {
                    borrows = query.IsDecensing
                        ? borrows.OrderByDescending(reser => reser.Book!.Author!.AuthorName)
                        : borrows.OrderBy(reser => reser.Book!.Author!.AuthorName);
                }
                if (query.SortBy == SortByReservation.Date)
                {
                    borrows = query.IsDecensing
                        ? borrows.OrderByDescending(reser => reser.IssueDate)
                        : borrows.OrderBy(reser => reser.IssueDate);
                }
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            var take = query.PageSize;
            borrows = borrows.Skip(skip).Take(take);
            return await borrows.ToListAsync();
        }
    }
}
