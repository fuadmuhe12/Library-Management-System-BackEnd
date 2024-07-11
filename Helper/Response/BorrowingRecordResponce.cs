using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Helper.Response
{
    public class BorrowingRecordResponce
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage {get; set;} 
    }
}