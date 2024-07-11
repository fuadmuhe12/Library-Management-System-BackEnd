using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Mail;
using Library_Management_System_BackEnd.Helper.Response;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_BackEnd.Repository
{
    public class BorrowingRecordRepository(
        LibraryContext context,
        IBookRepository bookRepo,
        IReservationRepository reservationRepo,
        UserManager<User> userManager,
        IEmailService emailService,
        INotificationRepository notificationRepo,
        IFineRepository fineRepo
    ) : IBorrowingRecordRepository
    {
        private readonly LibraryContext _context = context;
        private readonly IBookRepository _bookRepo = bookRepo;
        private readonly IReservationRepository _reservationRepo = reservationRepo;
        private readonly UserManager<User> _userManager = userManager;
        private readonly IEmailService _emailService = emailService;
        private readonly INotificationRepository _notificationRepo = notificationRepo;
        private readonly IFineRepository _fineRepo = fineRepo;

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
            var firstResevation = await _reservationRepo.GetCurrentReservation( bookId);
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

        private async Task RemindUser(string userId, int bookId, Reservation? firstResevation)
        {
            await _bookRepo.UpdateBookStatus(bookId, BookStatus.Reserved);

            var notification = await _notificationRepo.CreateNotification(
                userId,
                "Book Availability",
                $"Dear {firstResevation.User!.FirstName},\n\nWe are pleased to inform you that the book \"{firstResevation.Book!.Title}\" is now available for pickup. Please collect it within the next 5 hours to ensure it remains reserved for you. If you are unable to do so, the book will be made available to the next person in line.\n\nThank you for using our library services."
            );
            var mailRequest = notification.MapToMailRequest(firstResevation.User!.Email!);
            await _emailService.SendEmailAsync(mailRequest);
        }

        private async Task FineUser(string userId, BorrowingRecord? record)
        {
            var diff = (record.ReturnDate - record.DueDate).Value.Days;
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
    }
}
