using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Hangfire;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Helper.Mail;
using Library_Management_System_BackEnd.Interfaces;
using Serilog;

namespace Library_Management_System_BackEnd.Services
{
    public class BackgroundServiceLMS(
        IConfiguration config,
        IBorrowingRecordRepository borrowingRecord,
        IEmailService emailService,
        IFineRepository fineRepository,
        INotificationRepository notificationRepository
    ) : IBackgroundService
    {
        private readonly IConfiguration _config = config;
        private readonly IBorrowingRecordRepository _borrowingRecord = borrowingRecord;
        private readonly IEmailService _emailService = emailService;
        private readonly IFineRepository _fineRepo = fineRepository;
        private readonly INotificationRepository _notificationRepository = notificationRepository;

        /// <summary>
        /// Sends reminder emails to users whose borrowed books are nearing the deadline.
        /// </summary>
        public async Task RemindDeadline()
        {
            Log.Information("background Remind deadline is started ");
            var records = await _borrowingRecord.GetAllBorrowingRecord(
                DateTime.Now.AddDays(int.Parse(_config["AppConfig:DeadLineRemindDay"]!))
            );
            if (records != null)
            {
                foreach (var record in records)
                {
                    var mailRequest = new MailRequest
                    {
                        ToEmail = record.User!.Email!,
                        Subject = "Remind Book Deadline",
                        Body =
                            $"Dear {record.User.FirstName} \nThe Book {record.Book!.Title} you have borrowed has almost Reached it dead line Please Consider Returning the book"
                    };
                    var notification = mailRequest.MapToNotification(record.UserId);
                    await _notificationRepository.CreateNotification(
                        notification.UserId,
                        notification.Subject,
                        notification.Message
                    );

                    BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(mailRequest));
                }
            }
        }

        /// <summary>
        /// Sends reminder emails to users with unpaid fines for borrowed books.
        /// </summary>
        public async Task RemindFinePayment()
        {
            var fines = await _fineRepo.GetAllUnpaidFines();
            if (fines != null)
            {
                foreach (var fine in fines)
                {
                    var mailRequest = new MailRequest
                    {
                        ToEmail = fine.User!.Email!,
                        Subject = "Remind Book Fine",
                        Body =
                            $"Dear {fine.User.FirstName} \nThe Book {fine.Book!.Title} you have borrowed has a fine of ${fine.Amount} and before any compelecation we recommend you to pay your fines as soon as possible"
                    };
                    var notification = mailRequest.MapToNotification(fine.UserId);
                    await _notificationRepository.CreateNotification(
                        notification.UserId,
                        notification.Subject,
                        notification.Message
                    );
                    BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(mailRequest));
                }
            }
        }
    }
}
