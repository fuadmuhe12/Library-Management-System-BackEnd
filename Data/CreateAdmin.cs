using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.AuthDto;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Library_Management_System_BackEnd.Data
{
    public static class CreateAdmin
    {
        public static async void CreateAdminUser(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var adminUserDto = new RegisterDto
            {
                Email = Environment.GetEnvironmentVariable("ADMIN_EMAIL")!,
                Password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD")!,
                UserName = Environment.GetEnvironmentVariable("ADMIN_USERNAME")!,
                FirstName = Environment.GetEnvironmentVariable("ADMIN_FIRSTNAME")!,
                LastName = Environment.GetEnvironmentVariable("ADMIN_LASTNAME")!,
            };

            var AdminUser = adminUserDto.ToUserFromRegisterDto();
            if (userManager.FindByEmailAsync(adminUserDto.Email) is null)
            {
                var userCreateResponse = await userManager.CreateAsync(
                    AdminUser,
                    adminUserDto.Password
                );
                if (userCreateResponse.Succeeded)
                {
                    await userManager.AddToRoleAsync(AdminUser, "Admin");
                }
            }
        }
    }
}
