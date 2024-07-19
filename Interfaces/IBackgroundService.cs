using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Interfaces
{
    public interface IBackgroundService
    {
        public Task RemindDeadline();
        public Task RemindFinePayment();
    }
}