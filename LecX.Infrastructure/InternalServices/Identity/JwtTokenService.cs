using LecX.Application.Abstractions.InternalServices.Identity;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Auth.Common;
using LecX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LecX.Infrastructure.InternalServices.Identity
{
    public sealed class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAppDbContext _db;
        private readonly JwtSettings _settings;

        public JwtTokenService(
            UserManager<User> userManager,
            IOptions<JwtSettings> settings,
            IAppDbContext db)
        {
            _userManager = userManager;
            _db = db;
            _settings = settings.Value;
        }

        // ===== Public API =====

        /// <summary>
        /// Issue access + refresh token pair
        /// </summary>
        public async Task<AuthResult> IssueAsync(User user, string? clientIp, CancellationToken ct = default)
        {
            // 1) Access token
            var (accessToken, accessExp) = await GenerateAccessAsync(user);

            // 2) Refresh token
            var refreshPlain = RefreshTokenUtil.GeneratePlaintext();
            var refreshHash = RefreshTokenUtil.Hash(refreshPlain);
            var refreshExp = DateTime.Now.AddDays(_settings.ExpireDays);

            var rt = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = refreshHash,
                ExpiresAtUtc = refreshExp,
                CreatedByIp = clientIp
            };

            _db.Set<RefreshToken>().Add(rt);
            await _db.SaveChangesAsync(ct);

            // 3) Build ticket
            return await BuildTicketAsync(user, accessToken, accessExp, refreshPlain, refreshExp, ct);
        }

        /// <summary>
        /// Generate JWT access token
        /// </summary>
        public async Task<(string token, DateTime expiresUtc)> GenerateAccessAsync(User user)
        {
            var now = DateTime.Now;
            var expires = now.AddMinutes(_settings.ExpireMinutes);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new("sub",  user.Id),
                new("name", user.UserName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };
            // ClaimTypes.Role để [Authorize(Roles="...")] hoạt động chuẩn
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return (token, expires);
        }

        /// <summary>
        /// Build AuthResult (ticket)
        /// </summary>
        public async Task<AuthResult> BuildTicketAsync(
            User user,
            string accessToken,
            DateTime accessExp,
            string refreshPlain,
            DateTime refreshExp,
            CancellationToken ct)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResult
            {
                AccessToken = accessToken,
                AccessTokenExpiresUtc = accessExp,
                RefreshTokenPlain = refreshPlain,
                RefreshTokenExpiresUtc = refreshExp,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AvatarUrl = user.ProfileImagePath,
                    Roles = roles.ToList()
                }
            };
        }
    }
}
