using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Query.Enums;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_BackEnd.Repository
{
    public class ConstantRepository(LibraryContext context) : IConstantRepository
    {
        private readonly LibraryContext _context = context;

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Tag>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }
    }
}
