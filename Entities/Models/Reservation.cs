using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        required public string UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public bool NotificationSent { get; set; }
        public DateTime? NotificationTime { get; set; }
    }
}
