using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WizardFormBackend.Models;

namespace WizardFormBackend.Utils
{
    public class AuthProvider(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public string GetToken(User user, string RoleType)
        {
            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("RoleType", RoleType),
                new Claim(ClaimTypes.Role, RoleType)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"], _configuration["JWT:Audience"], claims, expires: DateTime.UtcNow.AddDays(8), signingCredentials: signIn
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
