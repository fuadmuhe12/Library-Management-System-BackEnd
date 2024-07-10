using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.BookDto;
using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Entities.Mapper
{
    public static class BookMapper
    {
        /*
        public int BookId { get; set; }
        required public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public BookStatus BookStatus { get; set; }
        public DateOnly PublicationYear { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
         */
        public static Book ToBookFromCreateDto(this CreateBookDto bookDto, string coverImagePath)
        {
            return new Book
            {
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                ISBN = bookDto.ISBN,
                CategoryId = bookDto.CategoryId,
                PublicationYear = bookDto.PublicationYear,
                Description = bookDto.Description,
                CoverImage = coverImagePath,
                BookStatus = BookStatus.Available,
            };
        }

        public static ViewBookDto ToViewFromBook(this Book book)
        {
            return new ViewBookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                AuthorName = book.Author!.AuthorName,
                ISBN = book.ISBN,
                Category = book.Category!.CategoryName,
                BookStatus = book.BookStatus.ToString(),
                PublicationYear = book.PublicationYear,
                Description = book.Description,
                CoverImage = book.CoverImage,
                Tags = book.BookTags!.Select(bookTag => bookTag.Tag!.MapToViewTagDto()).ToList()
            };
        }
    }
}
