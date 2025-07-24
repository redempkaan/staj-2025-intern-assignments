using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VBT_proje.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VBT_proje.Models;

namespace VBT_proje.Helpers
{
    public class JwtService
    {
        // Reading JWT key from appsettings.json
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Token generation method
        public string GenerateToken(User user)
        {
            // Defining claims for the token, can be added more if needed
            var claims = new[]
            {
                // User ID and username as claims
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Creating a symmetric security key using the JWT key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Signing credentials using the key and HMAC SHA256 hashing algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Creating the JWT token with claims, expiration time and signing credentials
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );
            // Converting the token to a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
