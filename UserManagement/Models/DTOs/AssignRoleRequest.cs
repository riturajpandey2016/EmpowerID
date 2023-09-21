#pragma warning disable

namespace UserManagement.Models.DTOs
{
    /// <summary>
    /// DTO: AssignRoleRequest
    /// </summary>
    public class AssignRoleRequest
    {
        public string Email { get; set; }

        public string Role {  get; set; }   
    }
}
