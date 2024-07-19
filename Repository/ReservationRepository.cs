using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Enums;
using Library_Management_System_BackEnd.Helper.Query;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_BackEnd.Repository
{
    public class ReservationRepository(LibraryContext context) : IReservationRepository
    {
        private readonly LibraryContext _context = context;

        public async Task CreateReservation(int bookId, string userId)
        {
            var reservation = new Reservation { BookId = bookId, UserId = userId };
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetAllReservation(ReservationQuery query)
        {
            var reservations = _context
                .Reservations.Include(reser => reser.Book)
                .ThenInclude(book => book!.Author)
                .Include(reser => reser.Book)
                .ThenInclude(book => book!.Category)
                .Include(reser => reser.Book)
                .ThenInclude(book => book!.BookTags)!
                .ThenInclude(bookTag => bookTag.Tag)
                .Include(reser => reser.User)
                .AsQueryable();
            if (query.FilterByReservationStaus != null)
            {
                reservations = reservations.Where(reser =>
                    reser.Status == query.FilterByReservationStaus
                );
            }
            if (query.Search && query.SearchValue != null)
            {
                switch (query.SearchBy)
                {
                    case SearchBy.Title:
                        reservations = reservations.Where(reser =>
                            reser.Book!.Title.Contains(query.SearchValue!)
                        );
                        break;
                    case SearchBy.Author:
                        reservations = reservations.Where(reser =>
                            reser.Book!.Author!.AuthorName.Contains(query.SearchValue!)
                        );
                        break;
                    case SearchBy.ISBN:
                        reservations = reservations.Where(reser =>
                            reser.Book!.ISBN.Contains(query.SearchValue!)
                        );
                        break;
                    default:
                        break;
                }
            }

            if (query.FilterByTags != null)
            {
                reservations = reservations.Where(reser =>
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
                    reservations = query.IsDecensing
                        ? reservations.OrderByDescending(reser => reser.Book!.Title)
                        : reservations.OrderBy(reser => reser.Book!.Title);
                }
                if (query.SortBy == SortByReservation.AuthorName)
                {
                    reservations = query.IsDecensing
                        ? reservations.OrderByDescending(reser => reser.Book!.Author!.AuthorName)
                        : reservations.OrderBy(reser => reser.Book!.Author!.AuthorName);
                }
                if (query.SortBy == SortByReservation.Date)
                {
                    reservations = query.IsDecensing
                        ? reservations.OrderByDescending(reser => reser.ReservationDate)
                        : reservations.OrderBy(reser => reser.ReservationDate);
                }
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            var take = query.PageSize;
            reservations = reservations.Skip(skip).Take(take);
            return await reservations.ToListAsync();
        }

        public async Task<Reservation?> GetBookReservation(int bookId)
        {
            var reservations = _context.Reservations.OrderBy(reser => reser.ReservationDate);
            try
            {
                return await reservations
                    .Where(reser =>
                        reser.Status == ReservationStatus.Pending && reser.BookId == bookId
                    )
                    .Include(res => res.Book)
                    .Include(res => res.User)
                    .FirstAsync();
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Task<List<Reservation>> GetBooksReservation(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation?> GetCurrentReservation(int bookId)
        {
            var reserv = _context
                .Reservations.Where(reser =>
                    reser.BookId == bookId && reser.Status == ReservationStatus.Pending
                )
                .Include(res => res.User)
                .Include(res => res.Book)
                .Include(res => res.Book!.Author)
                .Include(res => res.Book!.Category)
                .Include(res => res.Book!.BookTags)!
                .ThenInclude(bookTag => bookTag.Tag);
            return reserv.FirstOrDefaultAsync();
        }

        public async Task<Reservation?> GetReservationById(int id)
        {
            return await _context.Reservations.FirstOrDefaultAsync(reser =>
                reser.ReservationId == id
            );
        }

        public async Task<List<Reservation>> GetReservations(int bookId)
        {
            return await _context
                .Reservations.Include(res => res.Book)
                .Where(reser => reser.Status == ReservationStatus.Pending && reser.BookId == bookId)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetUserReservation(
            string userId,
            ReservationQuery query
        )
        {
            var reservations = _context
                .Reservations.Where(reser => reser.UserId == userId)
                .Include(reser => reser.Book)
                .ThenInclude(book => book!.Author)
                .Include(reser => reser.Book)
                .ThenInclude(book => book!.Category)
                .Include(reser => reser.Book)
                .ThenInclude(book => book!.BookTags)!
                .ThenInclude(bookTag => bookTag.Tag)
                .Include(reser => reser.User)
                .AsQueryable();
            if (query.FilterByReservationStaus != null)
            {
                reservations = reservations.Where(reser =>
                    reser.Status == query.FilterByReservationStaus
                );
            }
            if (query.Search && query.SearchValue != null)
            {
                switch (query.SearchBy)
                {
                    case SearchBy.Title:
                        reservations = reservations.Where(reser =>
                            reser.Book!.Title.Contains(query.SearchValue!)
                        );
                        break;
                    case SearchBy.Author:
                        reservations = reservations.Where(reser =>
                            reser.Book!.Author!.AuthorName.Contains(query.SearchValue!)
                        );
                        break;
                    case SearchBy.ISBN:
                        reservations = reservations.Where(reser =>
                            reser.Book!.ISBN.Contains(query.SearchValue!)
                        );
                        break;
                    default:
                        break;
                }
            }

            if (query.FilterByTags != null)
            {
                reservations = reservations.Where(reser =>
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
                    reservations = query.IsDecensing
                        ? reservations.OrderByDescending(reser => reser.Book!.Title)
                        : reservations.OrderBy(reser => reser.Book!.Title);
                }
                if (query.SortBy == SortByReservation.AuthorName)
                {
                    reservations = query.IsDecensing
                        ? reservations.OrderByDescending(reser => reser.Book!.Author!.AuthorName)
                        : reservations.OrderBy(reser => reser.Book!.Author!.AuthorName);
                }
                if (query.SortBy == SortByReservation.Date)
                {
                    reservations = query.IsDecensing
                        ? reservations.OrderByDescending(reser => reser.ReservationDate)
                        : reservations.OrderBy(reser => reser.ReservationDate);
                }
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            var take = query.PageSize;
            reservations = reservations.Skip(skip).Take(take);
            return await reservations.ToListAsync();
        }

        public async Task<bool> IsUserReservedBook(string userId, int bookId)
        {
            return await _context.Reservations.AnyAsync(reser =>
                reser.UserId == userId
                && reser.BookId == bookId
                && reser.Status == ReservationStatus.Pending
            );
        }

        public async Task UpdateReservationStatus(int id, ReservationStatus newStatus)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(reser =>
                reser.ReservationId == id
            );
            if (reservation != null)
            {
                reservation.Status = newStatus;
            }
            await _context.SaveChangesAsync();
        }
    }
}
