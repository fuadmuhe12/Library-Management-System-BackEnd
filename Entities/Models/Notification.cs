using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SentDate { get; set; } = DateTime.Now;
        public required string UserId { get; set; }
        public User? User { get; set; }
        public bool IsRead { get; set; } = false; //TODO: added
    }
}
