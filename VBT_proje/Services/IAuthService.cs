using VBT_proje.Models.DTOs;

namespace VBT_proje.Services
{
    // Interface defining the authentication service methods to be implemented by AuthService
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterDto dto);
        Task<AuthResultDto> LoginAsync(LoginDto dto);
        Task<bool> Logout(string username);



    }
}