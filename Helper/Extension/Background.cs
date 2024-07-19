using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Library_Management_System_BackEnd.Interfaces;
using Library_Management_System_BackEnd.Services;
using Microsoft.VisualBasic;

namespace Library_Management_System_BackEnd.Helper.Extension
{
    public static class Background
    {
        public static void RemindUser(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            IBackgroundService backservice =
                scope.ServiceProvider.GetRequiredService<IBackgroundService>();

            RecurringJob.AddOrUpdate(() => backservice.RemindDeadline(), Cron.Daily);
            RecurringJob.AddOrUpdate(() => backservice.RemindFinePayment(), Cron.Daily);
        }
    }
}
