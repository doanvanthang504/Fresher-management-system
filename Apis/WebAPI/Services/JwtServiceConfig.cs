using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace WebAPI.Services
{
    public static class JwtServiceConfig
    {
        public static void AddJwtConfig(this IServiceCollection service, IConfiguration configuration)
        {
            string key = configuration["Jwt:Key"];
            string issuer = configuration["Jwt:Issuer"];
            string audience = configuration["Jwt:Audience"];
            var secretKey = Encoding.UTF8.GetBytes(key);

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
