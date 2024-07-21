using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Dtos.AuthDto
{
    public class UserLoginResponceDto
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public IList<string> Role { get; set; } = new List<string>();
    }
}