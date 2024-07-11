using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Dtos.TagsDto;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Query;
using Library_Management_System_BackEnd.Helper.Query.Enums;
using Library_Management_System_BackEnd.Helper.Response;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_BackEnd.Repository
{
    public class BookRepository(LibraryContext context) : IBookRepository
    {
        private readonly LibraryContext _context = context;

        public async Task<AddTagsResponse> AddTagsAsync(BookTagDto bookTags)
        {
            try
            {
                await _context.BookTag.AddRangeAsync(bookTags.BookTags);
                await _context.SaveChangesAsync();
                return new AddTagsResponse
                {
                    IsSuccess = true,
                    SuccessMessage = "Tags added successfully"
                };
            }
            catch (System.Exception e)
            {
                return new AddTagsResponse { IsSuccess = false, ErrorMessage = e.Message };
            }
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            var rval = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            var createdBook = await _context
                .Books.Include(book => book.Author)
                .Include(book => book.Category)
                .Include(book => book.BookTags)!
                .ThenInclude(BookTag => BookTag.Tag)
                .FirstOrDefaultAsync(bk => bk.BookId == book.BookId);
            return createdBook!;
        }

        public async Task<bool> CategoryExit(int categoryId)
        {
            return await _context.Categories.AnyAsync(category =>
                category.CategoryId == categoryId
            );
        }

        public async Task<bool> AuthorExit(int authorId)
        {
            return await _context.Authors.AnyAsync(author => author.AuthorId == authorId);
        }

        public async Task<List<Book>> GetAllBooksAsync(BookQuery query)
        {
            var result = _context
                .Books.Include(book => book.Author)
                .Include(book => book.Category)
                .Include(book => book.BookTags)!
                .ThenInclude(BookTag => BookTag.Tag)
                .AsQueryable();
            if (query.Search && query.SearchValue != null)
            {
                if (query.SearchBy == SearchBy.Title && query.SearchValue != null)
                {
                    result = result.Where(book => book.Title.Contains(query.SearchValue));
                }
                if (query.SearchBy == SearchBy.Author)
                {
                    result = result.Where(book =>
                        book.Author!.AuthorName.Contains(query.SearchValue!)
                    );
                }
                if (query.SearchBy == SearchBy.ISBN)
                {
                    result = result.Where(book => book.ISBN.Contains(query.SearchValue!));
                }
            }

            if (query.FilterByCategory != null)
            {
                result = result.Where(book =>
                    book.Category!.CategoryName == query.FilterByCategory.ToString()
                );
            }

            if (query.FilterByTags != null)
            {
                result = result.Where(book =>
                    book.BookTags!.Any(bookTag =>
                        query
                            .FilterByTags.Select(tags => tags.ToString())
                            .Contains(bookTag.Tag!.TagName)
                    )
                );
            }

            if (query.SortBy != null)
            {
                if (query.SortBy == SortBy.BookName)
                {
                    result = query.IsDecensing
                        ? result.OrderByDescending(book => book.Title)
                        : result.OrderBy(book => book.Title);
                }
                if (query.SortBy == SortBy.AuthorName)
                {
                    result = query.IsDecensing
                        ? result.OrderByDescending(book => book.Author!.AuthorName)
                        : result.OrderBy(book => book.Author!.AuthorName);
                }
                if (query.SortBy == SortBy.PublishDate)
                {
                    result = query.IsDecensing
                        ? result.OrderByDescending(book => book.PublicationYear)
                        : result.OrderBy(book => book.PublicationYear);
                }
            }
            int skip = (query.PageNumber - 1) * query.PageSize;
            int take = query.PageSize;
            result = result.Skip(skip).Take(take);
            var final = await result.ToListAsync();

            return final;
        }

        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _context
                .Books.Include(book => book.Author)
                .Include(book => book.Category)
                .Include(book => book.BookTags)!
                .ThenInclude(BookTag => BookTag.Tag)
                .FirstOrDefaultAsync(book => book.BookId == bookId);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            var updatedBook = await _context
                .Books.Include(book => book.Author)
                .Include(book => book.Category)
                .Include(book => book.BookTags)!
                .ThenInclude(BookTag => BookTag.Tag)
                .FirstOrDefaultAsync(bk => bk.BookId == book.BookId);
            return updatedBook!;
        }

        public async Task<int> DeleteBookAsync(int bookId)
        {
            return await _context.Books.Where(book => book.BookId == bookId).ExecuteDeleteAsync();
        }

        public async Task<Book?> UpdateBookStatus(int bookId, BookStatus newStatus)
        {
            var book = await _context.Books.FirstOrDefaultAsync(book => book.BookId == bookId);
            if (book != null)
            {
                book.BookStatus = newStatus;
            }

            await _context.SaveChangesAsync();
            return book;
        }
    }
}
