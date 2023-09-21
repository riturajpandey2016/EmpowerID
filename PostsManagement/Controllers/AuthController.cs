using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostsManagement.Models;
using PostsManagement.Models.Dtos;
using PostsManagement.Services;

#pragma warning disable

namespace PostsManagement.Controllers
{
    [Route("api/[controller]/post")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;

        /// <summary>
        /// Constructor for the AuthController.
        /// </summary>
        /// <param name="authService">The authentication service for user registration, login, and role assignment.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDto">The registration DTO containing user registration data.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost("user/register")]
        public async Task<IActionResult> Register(RegistrationDto registerDto)
        { 
            ResponseDto result = await _authService.RegisterAsync(registerDto);
            if (result == null)
            {
                return Ok();
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequestDto">The login request DTO containing user credentials.</param>
        /// <returns>HTTP response containing login result or unauthorized status.</returns>
        [HttpPost("user/login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            ResponseDto result = await _authService.LoginAsync(loginRequestDto);
            if (result.IsSuccess)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(result.Result.ToString());
                return Ok(loginResponse);
            }
            return Unauthorized(result);
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="assignRoleRequest">The role assignment request DTO.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost("user/assign-role")]
        public async Task<IActionResult> AssignRole(AssignRoleRequest assignRoleRequest)
        {
            ResponseDto result = await _authService.AssignRoleAsync(assignRoleRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}