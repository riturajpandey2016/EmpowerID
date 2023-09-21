using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Models;

#pragma warning disable

namespace UserManagement.Services
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        public readonly JwtOptions _jwtOptions;

        /// <summary>
        /// Constructor for JWTTokenGenerator.
        /// </summary>
        /// <param name="jwtOptions">Options for JWT token generation.</param>
        public JWTTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Generates a JWT token for the given ApplicationUser.
        /// </summary>
        /// <param name="applicationUser">The ApplicationUser for whom the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public string GenerateToken(ApplicationUser applicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
              new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
