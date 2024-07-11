using Library_Management_System_BackEnd.Helper.Mail;
using Library_Management_System_BackEnd.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Library_Management_System_BackEnd.Services
{
      /// <summary>
    /// Represents a service for sending emails.
    /// </summary>
    public class EmailService(EmailSettings settings) : IEmailService
  
    {
        private readonly EmailSettings _settings = settings;

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="mailRequest">The mail request containing the email details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_settings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_settings.Mail, _settings.Password);
            var result = await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
