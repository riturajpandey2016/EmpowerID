using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.DTOs;
using UserManagement.Services;

#pragma warning disable

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        /// <summary>
        /// POST: api/auth/register
        /// TODO: This method register the user for authentication.
        /// </summary>
        /// <param name="registrationDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            var errorMessage = await _authService.Register(registrationDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok();
        }

        /// <summary>
        /// POST: api/auth/login
        /// TODO: This method is used for login.
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "UserName or Password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        /// <summary>
        /// TODO: This method assigns the role to particular user
        /// </summary>
        /// <param name="assignRoleRequest"></param>
        /// <returns></returns>
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(AssignRoleRequest assignRoleRequest)
        {
            var assignRoleSuccessfull = await _authService.AssignRole(assignRoleRequest.Email, assignRoleRequest.Role);
            if (!assignRoleSuccessfull)
            {
                _response.IsSuccess = false;
                _response.Message = "Error Encountered";
                return BadRequest(_response);
            }
            _response.Message = "Assigned Role Successfully";
            return Ok(_response);
        }
    }
}
