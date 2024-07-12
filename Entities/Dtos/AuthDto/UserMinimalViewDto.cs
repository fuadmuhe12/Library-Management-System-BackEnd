using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace Library_Management_System_BackEnd.Entities.Dtos.AuthDto
{
    public class UserMinimalViewDto
    {
        required public string UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}