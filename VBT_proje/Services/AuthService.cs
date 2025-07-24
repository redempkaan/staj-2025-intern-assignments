using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VBT_proje.Data;
using VBT_proje.Models;
using VBT_proje.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace VBT_proje.Services
{
    // Defining the AuthService
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Register method
        public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return new AuthResultDto { Success = false, Message = "Username already exists." };
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResultDto { Success = true, Message = "User registered successfully." };
        }

        // Login method
        public async Task<AuthResultDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null) return new AuthResultDto { Success = false, Message = "Invalid credentials." };

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return new AuthResultDto { Success = false, Message = "Invalid credentials." };
            }

            var token = CreateToken(user);
            return new AuthResultDto { Success = true, Message = "Login successful.", Token = token };
        }

        // Logout method
        public async Task<bool> Logout(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return false;

            return true;
        }

        // Token generation method
        private string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}