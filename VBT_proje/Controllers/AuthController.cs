using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VBT_proje.Models.DTOs;
using VBT_proje.Services;
using System.Threading.Tasks;
namespace VBT_proje.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Registration endpoint (POST /auth/register)
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
        // Login endpoint (POST /auth/login)
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (!result.Success) return Unauthorized(result);
            return Ok(result);
        }

        // Logout endpoint (POST /auth/logout) (requires authentication)
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return BadRequest("User not authenticated");

            var result = await _authService.Logout(username);
            if (!result)
                return BadRequest("Logout failed");

            return Ok("Successfully logged out");
        }
    }
}