using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Extensions;
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
        IReservationRepository reservationRepository
    ) : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository = reservationRepository;
        private readonly IBookRepository _bookRepository = bookRepository;

        /// <summary>
        /// Creates a reservation for a book with the specified book ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to create a reservation for.</param>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        /// [HttpPost]
        [Route("create/{bookId:int}")]
        public async Task<IActionResult> CreateReservation([FromRoute] int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            if (book.BookStatus != BookStatus.CheckedOut || book.BookStatus != BookStatus.Reserved)
            {
                return BadRequest("Book is not available for reservation");
            }

            var userId = User.GetUserId();
            await _reservationRepository.CreateReservation(bookId, userId);
            return Ok("Resevation Created Successfully");
        }
    }
}
