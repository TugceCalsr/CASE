using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceAPI.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateJwtToken(string username, IConfiguration config)
        {
            var secretKey = config["Jwt:Key"]; // appsettings.json'dan 'Jwt:Key' değerini al

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, username)
        }),
                Expires = DateTime.UtcNow.AddHours(1), // Token süresi
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
