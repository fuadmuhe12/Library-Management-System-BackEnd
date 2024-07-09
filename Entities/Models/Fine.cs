using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Models
{

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