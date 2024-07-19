using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Extensions;
using Library_Management_System_BackEnd.Helper.Query;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationController(
        IBookRepository bookRepository,
        IReservationRepository reservationRepository,
        IBorrowingRecordRepository borrowingRecordRepository
    ) : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository = reservationRepository;
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IBorrowingRecordRepository _borrowingRecordRepository =
            borrowingRecordRepository;

        /// <summary>
        /// Creates a reservation for a book with the specified book ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to create a reservation for.</param>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        /// [HttpPost]
        [Route("create/{bookId:int}")]
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromRoute] int bookId)
        {
            var userId = User.GetUserId();
            var book = await _bookRepository.GetBookByIdAsync(bookId);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            if (book.BookStatus != BookStatus.CheckedOut && book.BookStatus != BookStatus.Reserved)
            {
                return BadRequest(
                    $"Book is not available for reservation the current status is {book.BookStatus}"
                );
            }

            if (await _reservationRepository.IsUserReservedBook(userId, bookId))
            {
                return BadRequest("You have already reserved this book");
            }

            if (await _borrowingRecordRepository.IsUserBorrowedBook(userId, bookId))
            {
                return BadRequest("You have already borrowed this book");
            }

            await _reservationRepository.CreateReservation(bookId, userId);
            return Ok("Resevation Created Successfully");
        }

        [HttpGet]
        [Route("admin")]
        [Authorize]
        public async Task<IActionResult> GetReservationsAdmin([FromQuery] ReservationQuery query)
        {
            var reservations = await _reservationRepository.GetAllReservation(query);
            return Ok(reservations.Select(reser => reser.MapToViewReservation()));
        }

        [HttpGet]
        [Route("user")]
        [Authorize]
        public async Task<IActionResult> GetReservationsUser([FromQuery] ReservationQuery query)
        {
            var userId = User.GetUserId();
            var reservations = await _reservationRepository.GetUserReservation(userId, query);
            return Ok(reservations.Select(reser => reser.MapToViewReservation()));
        }

        [HttpPost]
        [Route("/cancel/{bookId:int}")]
        public async Task<IActionResult> CancelReservation([FromRoute] int bookId)
        {
            var userId = User.GetUserId();
            await _reservationRepository.CancelResevation(userId, bookId);
            return Ok();
        }
    }
}
