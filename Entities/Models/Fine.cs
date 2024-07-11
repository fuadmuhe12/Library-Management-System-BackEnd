using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Models
{
    public class Fine
    {
        public int FineId { get; set; }
        public required string UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        public DateTime? PaidDate { get; set; }
        public int BorrowingRecordId { get; set; } //TODO: add foreign key
        public BorrowingRecord? BorrowingRecord { get; set; } //TODO: add navigation property
        public bool IsPaid { get; set; } = false; //TODO: Fine Update
    }
}
