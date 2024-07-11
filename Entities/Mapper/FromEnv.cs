using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Helper.Mail;

namespace Library_Management_System_BackEnd.Entities.Mapper
{
    public static class FromEnv
    {
        public static EmailSettings MapToEmailSettings()
        {
            return new EmailSettings
            {
                Mail = Environment.GetEnvironmentVariable("SMTP_MAIL")!,
                DisplayName = Environment.GetEnvironmentVariable("SMTP_DISPLAYNAME")!,
                Password = Environment.GetEnvironmentVariable("SMTP_PASSWORD")!,
                Host = Environment.GetEnvironmentVariable("SMTP_HOST")!,
                Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")!)
            };
        }
    }
}
