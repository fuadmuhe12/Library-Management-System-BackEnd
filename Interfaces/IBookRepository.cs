using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.TagsDto;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Query;
using Library_Management_System_BackEnd.Helper.Response;

namespace Library_Management_System_BackEnd.Interfaces
{
    public interface IBookRepository
    {
        Task<AddTagsResponse> AddTagsAsync(BookTagDto bookTags);
        Task<Book> CreateBookAsync(Book book);
        Task<Book?> GetBookByIdAsync(int bookId);
        Task<List<Book>> GetAllBooksAsync(BookQuery query);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> AuthorExit(int authorId);
        Task<bool> CategoryExit(int categoryId);
        Task<int> DeleteBookAsync(int bookId);
    }
}
