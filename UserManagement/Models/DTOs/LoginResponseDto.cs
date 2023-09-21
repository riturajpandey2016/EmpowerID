#pragma warning disable

namespace UserManagement.Models.DTOs
{
    /// <summary>
    /// DTO: LoginResponseDto
    /// </summary>
    public class LoginResponseDto
    {
        public UserDto User { get; set; }   
        public string Token { get; set; }
    }
}
