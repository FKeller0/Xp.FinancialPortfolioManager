using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xp.FinancialPortfolioManager.Application.Common.Interfaces;
using Xp.FinancialPortfolioManager.Domain.Users;
using Xp.FinancialPortfolioManager.Infrastructure.Authentication.Claims;

namespace Xp.FinancialPortfolioManager.Infrastructure.Authentication.JwtGenerator
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Name, user.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("id", user.Id.ToString())
            };

            AddIds(user, claims);
            AddRoles(user, claims);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static void AddIds(User user, List<Claim> claims)
        {
            claims
                .AddIfValueNotNull("advisorId", user.AdvisorId?.ToString())
                .AddIfValueNotNull("clientId", user.ClientId?.ToString());            
        }

        private static void AddRoles(User user, List<Claim> claims)
        {
            user.GetProfileTypes().ForEach(type =>
            {
                claims.Add(new Claim("roles", type.Name));
            });
        }
    }
}