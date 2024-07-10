using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Helper
{
    public class SavaImageRespoce
    {
        public string? ImageName { get; set; }
        public bool IsSuccess { get; set; }
        public Exception? Error { get; set; }
    }
}
