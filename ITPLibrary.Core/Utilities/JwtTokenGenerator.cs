using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ITPLibrary.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ITPLibrary.Core.Utilities
{
    public class JwtTokenGenerator
    {
        public static string GenerateJwtToken(User user, string jwtSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.Name, user.UserId.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    ]
                ),
                Expires = DateTime.UtcNow.AddHours(1),
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
