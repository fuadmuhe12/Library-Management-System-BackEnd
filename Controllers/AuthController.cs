using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.AuthDto;
using Library_Management_System_BackEnd.Entities.Mapper;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Mail;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Library_Management_System_BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            IEmailService emailService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = registerDto.ToUserFromRegisterDto();
            var createResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (createResult.Succeeded)
            {
                var createdUser =  await _userManager.FindByEmailAsync(user.Email!);
                Log.Information($"new User created {registerDto.Email}");
                var roleResult = await _userManager.AddToRoleAsync(
                    user,
                    LMSUserRoles.User.ToString()
                );
                if (!roleResult.Succeeded)
                {
                    Log.Warning($"User Role Set Failed - Email => {registerDto.Email}");

                    return StatusCode(500, roleResult.Errors);
                }
                return Ok(
                    new UserLoginResponceDto
                    {
                        Token = await _tokenService.GenerateToken(user),
                        UserName = user.UserName!,
                        Role = await _userManager.GetRolesAsync(createdUser!)
                    }
                );
            }
            Log.Error(
                $"Failed to Create account - Email => {registerDto.Email} Errors=>  @{createResult.Errors}",
                createResult.Errors
            );
            return StatusCode(500, createResult.Errors);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            User? user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                Log.Warning($"User Not Found - UserName => {loginDto.UserName}");
                return Unauthorized("Username and/or password is incorrect");
            }
            var signInResult = await _signInManager.CheckPasswordSignInAsync(
                user,
                loginDto.Password,
                false
            );
            if (!signInResult.Succeeded)
            {
                Log.Warning(
                    $"User Login Failed  INCorrect Password - UserName => {loginDto.UserName}"
                );
                return Unauthorized("Username and/or password is incorrect");
            }

            return Ok(
                new UserLoginResponceDto
                {
                    Token = await _tokenService.GenerateToken(user),
                    UserName = user.UserName!,
                    Role = await _userManager.GetRolesAsync(user)
                }
            );
        }
    }
}
