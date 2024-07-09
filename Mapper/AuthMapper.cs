using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Dtos.AuthDto;
using Library_Management_System_BackEnd.Models;

namespace Library_Management_System_BackEnd.Mapper
{
    public static class AuthMapper
    {
        
        public static User ToUserFromRegisterDto(this RegisterDto dto){

            return new User{
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }
    }
}