
using CommentsManagement.Models;
using CommentsManagement.Models.Dtos;

namespace CommentsManagement.Services
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> RegisterAsync(RegistrationDto registrationDto);
        Task<ResponseDto> AssignRoleAsync(AssignRoleRequest assignRoleRequest);

    }
}
