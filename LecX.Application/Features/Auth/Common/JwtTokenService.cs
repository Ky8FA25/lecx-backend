using LecX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LecX.Application.Features.Auth.Common;

public interface IJwtTokenService
{
    Task<string> GenerateAsync(User user);
}

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _config;
    private readonly UserManager<User> _userManager;

    public JwtTokenService(IConfiguration config, UserManager<User> userManager)
    {
        _config = config;
        _userManager = userManager;
    }

    public async Task<string> GenerateAsync(User user)
    {
        var issuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("Missing Jwt:Issuer");
        var audience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("Missing Jwt:Audience");
        var secret = _config["Jwt:Key"] ?? throw new InvalidOperationException("Missing Jwt:Key");
        if (secret.Length < 32) throw new InvalidOperationException("Jwt:Key should be at least 32 characters.");

        var expiresMinutes = int.TryParse(_config["Jwt:ExpiresMinutes"], out var m) ? m : 60;
        var now = DateTime.UtcNow;

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new("sub",  user.Id),
            new("name", user.UserName ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
        };
        claims.AddRange(roles.Select(r => new Claim("role", r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: now,
            expires: now.AddMinutes(expiresMinutes),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
