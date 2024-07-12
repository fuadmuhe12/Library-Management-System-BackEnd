using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.AuthDto;
using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Entities.Mapper
{
    public static class AuthMapper
    {
        public static User ToUserFromRegisterDto(this RegisterDto dto)
        {
            return new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }

        public static UserMinimalViewDto MapToUserMinimalDto(this User user)
        {
            return new UserMinimalViewDto
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email
            };
        }
    }
}
