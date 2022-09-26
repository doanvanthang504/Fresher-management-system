using Application.Interfaces;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.ViewModels.TokenObjectViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructures.Services
{
    public class JwtService : IJwtService
    {

        IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenObject GenerateToken(User user)
        {
            var sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            string issuer = _configuration["Jwt:Issuer"];
            string audience = _configuration["Jwt:Audience"];
            var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim(ClaimTypes.Name,user.Username!),
                    new Claim(ClaimTypes.Role,user.Role.ToString()),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer, 
                audience,
                claims,
                expires: DateTime.Now.AddHours(Constant.JWT_TOKEN_EXPIRITION),
                signingCredentials: credentials
                );

            var expires = DateTimeOffset.Now.AddHours(Constant.JWT_TOKEN_EXPIRITION).ToUnixTimeSeconds();
            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);

            var tokenObject = new TokenObject
            {
                Token = tokenResult,
                Expires = expires
            };

            return tokenObject;
        }
    }
}
