using LecX.Application.Abstractions; // IAppDbContext, IJwtTokenService
using LecX.Application.Features.Auth.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LecX.Application.Features.Auth.GoogleCallBack
{
    public sealed class GoogleCallbackHandler(
        UserManager<User> userManager,
        IAppDbContext db,
        IJwtTokenService jwtService,
        IConfiguration config
    ) : IRequestHandler<GoogleCallbackCommand, GoogleCallbackResult>
    {
        public async Task<GoogleCallbackResult> Handle(GoogleCallbackCommand req, CancellationToken ct)
        {
            // 1) tìm user theo email (Google luôn có email)
            var user = await userManager.FindByEmailAsync(req.Email);

            if (user is null)
            {
                user = new User
                {
                    UserName = req.Email,
                    Email = req.Email,
                    FirstName = req.FirstName ?? req.Email,
                    LastName = req.LastName ?? string.Empty,
                    ProfileImagePath = req.AvatarUrl,
                    EmailConfirmed = true
                };

                var create = await userManager.CreateAsync(user);
                if (!create.Succeeded)
                    throw new InvalidOperationException(create.Errors.First().Description);

                await userManager.AddToRoleAsync(user, "Student");
            }

            // 2) đảm bảo liên kết external
            var existing = await userManager.FindByLoginAsync(req.Provider, req.ProviderKey);
            if (existing is null)
            {
                var add = await userManager.AddLoginAsync(user, new UserLoginInfo(req.Provider, req.ProviderKey, req.Provider));
                if (!add.Succeeded)
                    throw new InvalidOperationException(add.Errors.First().Description);
            }

            // 3) JWT
            var jwt = await jwtService.GenerateAsync(user);

            // 4) refresh token
            var refreshPlain = RefreshTokenUtil.GeneratePlaintext();
            var refreshHash = RefreshTokenUtil.Hash(refreshPlain);
            var expireDays = int.Parse(config["Jwt:ExpireDays"] ?? "14");
            var refreshExp = DateTime.UtcNow.AddDays(expireDays);

            db.Set<RefreshToken>().Add(new RefreshToken
            {
                UserId = user.Id,
                TokenHash = refreshHash,
                ExpiresAtUtc = refreshExp,
                CreatedByIp = req.ClientIp
            });
            await db.SaveChangesAsync(ct);

            var roles = await userManager.GetRolesAsync(user);

            return new GoogleCallbackResult(
                Jwt: jwt,
                RefreshTokenPlain: refreshPlain,
                RefreshTokenExpiresUtc: refreshExp,
                User: new UserDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AvatarUrl = user.ProfileImagePath,
                    Roles = roles.ToList()
                },
                Roles: roles
            );
        }
    }
}