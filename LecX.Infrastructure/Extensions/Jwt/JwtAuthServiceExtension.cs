using LecX.Application.Features.Auth.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LecX.Infrastructure.Extensions.Jwt

{
    public static class JwtAuthServiceExtension
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, IConfiguration config)
        {
            var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            services.AddAuthorization();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            return services;
        }
    }
}
