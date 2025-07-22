using LoginAPI.Models;
using LoginAPI.Models.DTOs;

public interface IAuthService
{
    Task<bool> UserExists(string username);
    Task<User> Register(RegisterDto dto);
    Task<string> Login(LoginDto dto);
}
