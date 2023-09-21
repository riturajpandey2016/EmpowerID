#pragma warning disable

namespace PostsManagement.Models.Dtos
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
