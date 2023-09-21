#pragma warning disable

namespace UserManagement.Models.DTOs
{
    /// <summary>
    /// DTO: RegistrationDto
    /// </summary>
    public class RegistrationDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }
}
