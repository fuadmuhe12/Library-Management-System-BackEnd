using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_BackEnd.Repository
{
    public class AuthorRepository(LibraryContext context) : IAuthorRepository
    {
        private readonly LibraryContext _context = context;

        public async Task<Author> AddAuthor(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<List<Author>> GetAllAuthors(AuthorQuery query)
        {
            var result = _context.Authors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.AuthorNameSearch))
            {
                result = result.Where(au => au.AuthorName.Contains(query.AuthorNameSearch));
            }
            result = query.IsDecensing
                ? result.OrderByDescending(au => au.AuthorName)
                : result.OrderBy(au => au.AuthorName);
            var skip = (query.PageNumber - 1) * query.PageSize;

            return await result.Skip(skip).Take(query.PageSize).ToListAsync();
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            var result = await _context.Authors.FindAsync(id);
            return result;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public Task<bool> AuthorExists(int id)
        {
            return _context.Authors.AnyAsync(au => au.AuthorId == id);
        }

        public async Task<int> DeleteAuthor(int id)
        {
            return await _context.Authors.Where(au => au.AuthorId == id).ExecuteDeleteAsync();
        }
    }
}
