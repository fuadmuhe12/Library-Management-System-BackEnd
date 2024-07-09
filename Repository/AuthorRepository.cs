using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Interfaces;
using Library_Management_System_BackEnd.Models;
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

        public async Task<List<Author>> GetAllAuthors()
        {
            var result = await _context.Authors.ToListAsync();
            return result;
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            var result = await _context.Authors.FindAsync(id);
            return result;
        }
    }
}
