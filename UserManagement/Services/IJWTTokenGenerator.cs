using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
