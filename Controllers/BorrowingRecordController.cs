using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.BorrowingRecordDto;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Extensions;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BorrowingRecordController(
        IBookRepository bookRepo,
        IBorrowingRecordRepository borrowingRecord
    ) : ControllerBase
    {
        private readonly IBookRepository _bookRepo = bookRepo;
        private readonly IBorrowingRecordRepository _borrowingRecord = borrowingRecord;

        [HttpPost]
        [Route("{bookId:int}")]
        public async Task<IActionResult> CreateBorrowingRecord([FromRoute] int bookId)
        {
            var userId = User.GetUserId();
            var book = await _bookRepo.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            var bookStatus = book.BookStatus;
            if (bookStatus != BookStatus.Available)
            {
                return BadRequest("Book is not available");
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
    }
}
