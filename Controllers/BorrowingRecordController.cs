using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.BorrowingRecordDto;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Extensions;
using Library_Management_System_BackEnd.Helper.Query;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Library_Management_System_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BorrowingRecordController(
        IBookRepository bookRepo,
        IBorrowingRecordRepository borrowingRecord,
        IReservationRepository reservationRepo
    ) : ControllerBase
    {
        private readonly IBookRepository _bookRepo = bookRepo;
        private readonly IBorrowingRecordRepository _borrowingRecord = borrowingRecord;
        private readonly IReservationRepository _reservationRepo = reservationRepo;

        /// <summary>
        /// Creates a borrowing record for a book with the specified book ID.
        /// also Update the Rerservation if the current book is reserved by the user.
        /// </summary>
        /// <param name="bookId">The ID of the book.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        /// [HttpPost]
        [Route("{bookId:int}")]
        [HttpPost]
        public async Task<IActionResult> CreateBorrowingRecord([FromRoute] int bookId)
        {
            var userId = User.GetUserId();
            var book = await _bookRepo.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            var bookStatus = book.BookStatus;
            if (bookStatus != BookStatus.Available && bookStatus != BookStatus.Reserved)
            {
                return BadRequest("Book is not available");
            }
            /// updating reservatio if the current book is reserved and the user is the reserver
            if (book.BookStatus == BookStatus.Reserved)
            {
                var reservation = await _reservationRepo.GetCurrentReservation(bookId);
                if (reservation != null && reservation.UserId != userId)
                {
                    return BadRequest("Book Is Reserved By Other Person");
                }
                await _reservationRepo.UpdateReservationStatus(
                    reservation!.ReservationId,
                    ReservationStatus.Completed
                );
            }
            var borrowingRecordDto = new CreateBorrowingRecordDto
            {
                BookId = bookId,
                UserId = userId
            };
            var RecordResponse = await _borrowingRecord.CreateBorrowingRecord(
                borrowingRecordDto.MapToBorrowingRecord()
            );
            if (RecordResponse.IsSuccess)
            {
                return Ok("Successfull");
            }
            return StatusCode(500, RecordResponse.ErrorMessage);
        }

        [HttpPost]
        [Route("return/{bookId:int}")]
        public async Task<IActionResult> ReturnBook([FromRoute] int bookId)
        {
            var userId = User.GetUserId();
            var book = await _bookRepo.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            if (!await _borrowingRecord.IsUserBorrowedBook(userId, bookId))
            {
                return BadRequest("You have not borrowed this book");
            }
            try
            {
                await _borrowingRecord.ReturnBook(userId, bookId);
                return Ok("Book Returned Successfully");
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetBorrowedBooksUser(
            [FromQuery] BorrowingRecordQuery query
        )
        {
            var userId = User.GetUserId();
            var borrowingResult = await _borrowingRecord.GetAllUserBorrowRecord(userId, query);
            var finalviewBorrows = borrowingResult.Select(record => record.MapToViewRecord());
            return Ok(finalviewBorrows);
        }
        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBorrowedBooksAdmin(
            [FromQuery] BorrowingRecordQuery query
        )
        {
            var userId = User.GetUserId();
            var borrowingResult = await _borrowingRecord.GetAllBorrowRecord( query);
            var finalviewBorrows = borrowingResult.Select(record => record.MapToViewRecord());
            return Ok(finalviewBorrows);
        }
    }
}
