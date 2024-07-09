using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Models
{
    /* 6. BorrowingRecords
Tracks the borrowing activity of users.

RecordID: Primary key, unique identifier.
UserID: Foreign key to the Users table.
BookID: Foreign key to the Books table.
IssueDate: Date the book was issued.
DueDate: Date the book is due for return.
ReturnDate: Date the book was returned (nullable).
FineAmount: Fine amount for overdue returns (nullable). */
    public class BorrowingRecord
    {
        public int BorrowingRecordId { get; set; }
        required public string UserId { get; set; } 
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14);
        public DateTime? ReturnDate { get; set; }
        public decimal? FineAmount { get; set; }
        
    }
}