using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentDate { get; set; } = DateTime.Now;
        required public string UserId { get; set; } 
        public User? User { get; set; }
    }
}
