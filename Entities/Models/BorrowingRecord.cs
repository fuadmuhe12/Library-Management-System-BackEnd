using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Models
{
    public class BorrowingRecord
    {
        public int BorrowingRecordId { get; set; }
        public required string UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddMicroseconds(14);
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false; //TODO: BorrowingRecord Update
    }
}
