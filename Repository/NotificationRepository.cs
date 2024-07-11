using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Interfaces;

namespace Library_Management_System_BackEnd.Repository
{
    public class NotificationRepository(LibraryContext context) : INotificationRepository
    {
        private readonly LibraryContext _context = context;

        public async Task<Notification> CreateNotification(
            string userId,
            string subject,
            string message
        )
        {
            var notification = new Notification
            {
                UserId = userId,
                Subject = subject,
                Message = message
            };
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
