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
    public class FineRepository(LibraryContext context) : IFineRepository
    {
        private readonly LibraryContext _context = context;

        public async Task<Fine> CreateFine(Fine fine)
        {
            await _context.Fines.AddAsync(fine);
            await _context.SaveChangesAsync();
            return _context.Fines.FirstOrDefault(f =>
                f.BorrowingRecordId == fine.BorrowingRecordId
            )!;
        }

        public async Task<List<Fine>> GetAllUnpaidFines()
        {
            return await _context
                .Fines.Include(fine => fine.Book)
                .Include(fine => fine.Book!.Author)
                .Include(fine => fine.User)
                .Where(fine => fine.IsPaid == false)
                .ToListAsync();
        }
    }
}
