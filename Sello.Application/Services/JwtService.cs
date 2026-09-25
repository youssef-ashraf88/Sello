using Sello.Application.DTO;
using Sello.Application.ServiceContracts;
using Sello.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Sello.Application.Settings;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace Sello.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public LoginResponse CreateJwtToken(ApplicationUser user, string? role)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.EXPIRATION_MINUTES));

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email!),
                new Claim(ClaimTypes.Name, user.PersonName!),
                new Claim(ClaimTypes.Role, role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenGenerator = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new LoginResponse
            {
                PersonName = user.PersonName,
                Email = user.Email,
                Role = role,
                Token = token,
                Expiration = expiration
            };
        }
    }
}
