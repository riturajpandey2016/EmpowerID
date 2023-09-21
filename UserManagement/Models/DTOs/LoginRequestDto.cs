#pragma warning disable

namespace UserManagement.Models.DTOs
{
    /// <summary>
    /// DTO: LoginRequestDto
    /// </summary>
    public class LoginRequestDto
    {
        public string UserName { get; set; }    

        public string Password { get; set; }    
    }
}
