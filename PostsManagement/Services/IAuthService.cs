
using PostsManagement.Models;
using PostsManagement.Models.Dtos;

namespace PostsManagement.Services
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> RegisterAsync(RegistrationDto registrationDto);
        Task<ResponseDto> AssignRoleAsync(AssignRoleRequest assignRoleRequest);

    }
}
