using UserManagement.Models.DTOs;

#pragma warning disable

namespace UserManagement.Services
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationDto registrationDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
