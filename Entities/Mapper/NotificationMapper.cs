using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Mail;

namespace Library_Management_System_BackEnd.Entities.Mapper
{
    public static class NotificationMapper
    {
        public static MailRequest MapToMailRequest(this Notification notification, string toEmail)
        {
            return new MailRequest
            {
                ToEmail = toEmail ,
                Subject = notification.Subject,
                Body = notification.Message
            };
        }
        public static Notification MapToNotification(this MailRequest mail,string userID)
        {
            return new Notification
            {
               UserId = userID,
               Message = mail.Body,
               Subject = mail.Subject,

            };
        }
    }
}