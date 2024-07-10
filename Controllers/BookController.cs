using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.BookDto;
using Library_Management_System_BackEnd.Entities.Dtos.TagsDto;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Helper.Query;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System_BackEnd.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BookController(IImageService imageService, IBookRepository bookRepo)
        : ControllerBase
    {
        private readonly IImageService _imageService = imageService;
        private readonly IBookRepository _bookRepo = bookRepo;
        private const string _GetBookpath = "GetBookById";

        /*  [HttpPost]
         [Route("bookCoverImage")]
         public async Task<IActionResult> createCoverImage([FromForm] IFormFile imageFile)
         {
             var responce = await _imageService.SaveImageAsync(imageFile);
             if (responce.IsSuccess)
             {
                 return Ok(responce);
             }
             return BadRequest(responce.Error!.Message);
         } */

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDto BookDto)
        {
            var saveImageResponce = await _imageService.SaveImageAsync(BookDto.CoverImage);
            if (!saveImageResponce.IsSuccess)
            {
                return BadRequest(saveImageResponce.Error!.Message);
            }
            var book = BookDto.ToBookFromCreateDto(saveImageResponce.ImageName!);
            var createdBook = await _bookRepo.CreateBookAsync(book);

            return CreatedAtRoute(
                _GetBookpath,
                new { BookId = createdBook.BookId },
                createdBook.ToViewFromBook()
            );
        }

        [HttpPost]
        [Route("addTags/{bookId:int}")]
        public async Task<IActionResult> AddTags(
            [FromForm] AddTagsDto tagsDto,
            [FromRoute] int bookId
        )
        {
            var bookTags = tagsDto.MapToBookTagDto(bookId);
            var addResponce = await _bookRepo.AddTagsAsync(bookTags);
            if (!addResponce.IsSuccess)
            {
                return BadRequest(addResponce.ErrorMessage);
            }
            return Ok(addResponce.SuccessMessage);
        }

        [HttpGet]
        [Route("{bookId:int}", Name = _GetBookpath)]
        public async Task<IActionResult> GetBookById([FromRoute] int bookId)
        {
            var book = await _bookRepo.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book.ToViewFromBook());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] BookQuery query)
        {
            var books = await _bookRepo.GetAllBooksAsync(query);
            return Ok(books.Select(book => book.ToViewFromBook()));
        }

        [HttpPut]
        [Route("{bookId:int}")]
        public Task<IActionResult> UpdateBook([FromRoute] int bookId, UpdateBookDto bookDto) { }
    }
}
