using PostsManagement.DTOs;
using PostsManagement.Models;
using PostsManagement.Models.Dtos;
using PostsManagement.Utility;

#pragma warning disable

namespace PostsManagement.Services
{
    public class AuthService : IAuthService
    {
        public readonly IBaseService _baseService;

        /// <summary>
        /// Constructor for the AuthService.
        /// </summary>
        /// <param name="baseService">The base service used for sending HTTP requests.</param>
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Assigns a role to a user asynchronously.
        /// </summary>
        /// <param name="assignRoleRequest">The role assignment request DTO.</param>
        /// <returns>Response DTO indicating the result of the role assignment.</returns>
        public async Task<ResponseDto> AssignRoleAsync(AssignRoleRequest assignRoleRequest)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
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
                ApiType = Utility.SD.ApiType.POST,
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
                ApiType = Utility.SD.ApiType.POST,
                Data = registrationDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
