using Microsoft.AspNetCore.Identity;

#pragma warning disable

namespace UserManagement.Models
{
    /// <summary>
    /// DTO: This object adds Name Column in AspNetUsers table
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
