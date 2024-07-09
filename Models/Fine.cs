using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Models
{
    /* 9. Fines
Tracks fines for overdue book returns.

FineID: Primary key, unique identifier.
UserID: Foreign key to the Users table.
BookID: Foreign key to the Books table.
Amount: Fine amount.
PaidDate: Date the fine was paid (nullable). */
    public class Fine
    {
        public int FineId { get; set; }
        required public string UserId { get; set; } 
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidDate { get; set; }
        
    }
}