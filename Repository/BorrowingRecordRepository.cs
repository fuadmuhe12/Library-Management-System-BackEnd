using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Response;
using Library_Management_System_BackEnd.Interfaces;

namespace Library_Management_System_BackEnd.Repository
{
    public class BorrowingRecordRepository(LibraryContext context, IBookRepository bookRepo)
        : IBorrowingRecordRepository
    {
        private readonly LibraryContext _context = context;
        private readonly IBookRepository _bookRepo = bookRepo;

        public async Task<BorrowingRecordResponce> CreateBorrowingRecord(BorrowingRecord record)
        {
            try
            {
                await _context.BorrowingRecords.AddAsync(record);
                await _bookRepo.UpdateBookStatus(record.BookId, BookStatus.CheckedOut);
                return new BorrowingRecordResponce { IsSuccess = true };
            }
            catch (System.Exception er)
            {
                return new BorrowingRecordResponce { IsSuccess = false, ErrorMessage = er.Message };
            }
        }
    }
}
