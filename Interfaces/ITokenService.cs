using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}