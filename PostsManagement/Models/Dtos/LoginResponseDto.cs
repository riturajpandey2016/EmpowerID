#pragma warning disable

namespace PostsManagement.Models.Dtos
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
