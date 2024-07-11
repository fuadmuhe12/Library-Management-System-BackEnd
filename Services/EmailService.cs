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

            // Create a HTML email body with inline CSS
            var htmlBody =
                $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
            }}
            .container {{
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 20px;
                border: 1px solid #ccc;
                border-radius: 10px;
                background-color: #f9f9f9;
            }}
            .header {{
                font-size: 1.5em;
                font-weight: bold;
                margin-bottom: 20px;
            }}
            .content {{
                margin-bottom: 20px;
            }}
            .footer {{
                font-size: 0.9em;
                color: #777;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>{mailRequest.Subject}</div>
            <div class='content'>
               {mailRequest.Body}
            </div>
            <div class='footer'>
                Best regards,<br/>
                Your Library Team
            </div>
        </div>
    </body>
    </html>";

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_settings.Mail, _settings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
