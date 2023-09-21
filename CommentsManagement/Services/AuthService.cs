using CommentsManagement.DTOs;
using CommentsManagement.Models;
using CommentsManagement.Models.Dtos;
using CommentsManagement.Services;
using CommentsManagement.Utility;
using static CommentsManagement.Utility.SD;

#pragma warning disable
namespace PostsManagement.Services
{
    public class AuthService : IAuthService
    {
        public readonly IBaseService _baseService;

        /// <summary>
        /// Constructor for AuthService.
        /// </summary>
        /// <param name="baseService">The service responsible for making API requests.</param>
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Assigns a role to a user asynchronously.
        /// </summary>
        /// <param name="assignRoleRequest">The request containing user email and role to assign.</param>
        /// <returns>The response from the API call.</returns>
        public async Task<ResponseDto> AssignRoleAsync(AssignRoleRequest assignRoleRequest)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = assignRoleRequest,
                Url = SD.AuthAPIBase + "/api/auth/assign-role"
            });

        }

        /// <summary>
        /// Logs in a user asynchronously.
        /// </summary>
        /// <param name="loginRequestDto">The login request DTO containing user credentials.</param>
        /// <returns>Response DTO containing the login result.</returns>
        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            });
        }

        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="registrationDto">The registration DTO containing user registration data.</param>
        /// <returns>Response DTO indicating the result of the registration.</returns>
        public async Task<ResponseDto> RegisterAsync(RegistrationDto registrationDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = registrationDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
