using LecX.Application.Abstractions;
using LecX.Application.Features.Auth.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace LecX.Application.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, AuthResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAppDbContext _db;
        private readonly IJwtTokenService _jwt;
        private readonly IConfiguration _config;

        public LoginHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAppDbContext db,
            IJwtTokenService jwt,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _jwt = jwt;
            _config = config;
        }

        public async Task<AuthResponse> Handle(LoginRequest req, CancellationToken ct)
        {
            var user = await _userManager.FindByNameAsync(req.EmailOrUserName)
                       ?? await _userManager.FindByEmailAsync(req.EmailOrUserName);
            if (user is null)
                throw new UnauthorizedAccessException("User does not exist");

            var pwResult = await _signInManager.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: true);
            if (!pwResult.Succeeded)
                throw new UnauthorizedAccessException("Wrong password");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                throw new UnauthorizedAccessException("Email not confirmed");

            // Access token
            var jwt = await _jwt.GenerateAsync(user);
            var parsed = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

            // Refresh token
            var refreshPlain = RefreshTokenUtil.GeneratePlaintext();
            var refreshHash = RefreshTokenUtil.Hash(refreshPlain);

            var expiresUtc = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"] ?? "60"));

            var rt = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = refreshHash,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:ExpireDays"] ?? "7")),
                CreatedByIp = req.RequestIp
            };
            _db.Set<RefreshToken>().Add(rt);
            await _db.SaveChangesAsync(ct);

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                AccessToken = jwt,
                AccessTokenExpiresUtc = expiresUtc,
                RefreshTokenPlain = refreshPlain,
                RefreshTokenExpiresUtc = rt.ExpiresAtUtc,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AvatarUrl = user.ProfileImagePath,
                    Roles = roles.ToList()
                },
                ReturnUrl = req.ReturnUrl ?? "/"
            };
        }
    }
}
