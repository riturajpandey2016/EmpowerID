#pragma warning disable

namespace UserManagement.Models
{
    /// <summary>
    /// DTO: This object defines the Issuer, Audience and Secret Key for JWT Authentication
    /// </summary>
    public class JwtOptions
    {
        public string Issuer { get; set; } 
        public string Audience { get; set; }   
        public string Secret { get; set; }   
    }
}
