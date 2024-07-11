using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.BorrowingRecordDto;
using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Entities.Mapper
{
    public static class BorrowingMapper
    {
        public static BorrowingRecord MapToBorrowingRecord(this CreateBorrowingRecordDto recordDto)
        {
            return new BorrowingRecord { UserId = recordDto.UserId, BookId = recordDto.BookId };
        }

        public static Fine MapToFine(this BorrowingRecord record, decimal amount)
        {
            return new Fine
            {
                UserId = record.UserId,
                BookId = record.BookId,
                BorrowingRecordId = record.BorrowingRecordId
            };
        }
    }
}
