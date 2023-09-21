using Microsoft.AspNetCore.Identity;
using UserManagement.Models;
using UserManagement.Models.DTOs;

#pragma warning disable

namespace UserManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJWTTokenGenerator _jWTToken;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jWTToken"></param>
        public AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJWTTokenGenerator jWTToken)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager; 
            _jWTToken = jWTToken;
        }

        /// <summary>
        /// Assigns a role to a user based on their email.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="roleName">The name of the role to assign.</param>
        /// <returns>True if the role was successfully assigned, false otherwise.</returns>
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null) 
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Handles user login and generates a JWT token upon successful login.
        /// </summary>
        /// <param name="loginRequestDto">The login request DTO containing user credentials.</param>
        /// <returns>A login response DTO with user information and JWT token.</returns>
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || isValid == false)
            {
                return new LoginResponseDto { User = null, Token = "" };
            }

            var token = _jWTToken.GenerateToken(user);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        /// <summary>
        /// Handles user registration.
        /// </summary>
        /// <param name="registrationDto">The registration DTO containing user registration data.</param>
        /// <returns>An error message if registration fails; otherwise, an empty string for successful registration.</returns>
        public async Task<string> Register(RegistrationDto registrationDto)
        {
            try
            {
                ApplicationUser user = new()
                {
                    UserName = registrationDto.Email,
                    Email = registrationDto.Email,
                    NormalizedEmail = registrationDto.Email.ToUpper(),
                    Name = registrationDto.Name,
                    PhoneNumber = registrationDto.PhoneNumber,
                };


                var result = await _userManager.CreateAsync(user, registrationDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _context.ApplicationUsers.First(x => x.UserName == registrationDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex) 
            {
                return "Error Encountered";
            }
        }
    }
}
