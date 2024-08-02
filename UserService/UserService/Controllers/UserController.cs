using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Runtime.InteropServices;
using UserService.Dto;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.GetUserAsync(loginDto.Username);
            if (user == null || user.password != loginDto.Password)
            {
                return Unauthorized();
            }
            return Ok(new { Username = user.username, Role = user.Role });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return BadRequest("Password do not match");
            }
            var existingUser = await _userRepository.GetUserAsync(registerDto.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists.");
            }
            var newUser = new User
            {
                username = registerDto.Username,
                password = registerDto.Password,
                email = registerDto.Email,
                Role = registerDto.Role,
            };
            await _userRepository.AddUserAsync(newUser);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return NotFound("Email not found.");

            return Ok(new { Password = user.password});
        }
    }
}