using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SearchMaster.Core;
using SearchMaster.Core.Enum;
using SearchMaster.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SearchMaster.Infrastructure
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public string GenerateLoginToken(string email)
        {
            Claim[] claims = [
                new(ClaimsIdentity.DefaultRoleClaimType, Roles.ConfirmingEmail.ToString()),
                new(Strings.Email, email),
            ];

            DateTime dateTime = DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes);

            return CreateToken(claims, dateTime);
        }

        public string GenerateRegistrationToken(string email)
        {
            Claim[] claims = [
                new(Strings.Email, email),
                new(ClaimsIdentity.DefaultRoleClaimType, Roles.Registering.ToString()),
            ];

            DateTime dateTime = DateTime.UtcNow.AddHours(_options.ExpiresHours);

            return CreateToken(claims, dateTime);
        }

        public string GenerateToken(Person person)
        {
            Claim[] claims = [
                new(Strings.UserId, person.Id.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString()),
            ];

            DateTime dateTime = DateTime.UtcNow.AddHours(_options.ExpiresHours);

            return CreateToken(claims, dateTime);
        }

        private string CreateToken(Claim[] claims, DateTime dateTime)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: dateTime);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
