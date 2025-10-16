using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Auth.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Auth.Logout
{
    public sealed class LogoutHandler(IAppDbContext db)
        : IRequestHandler<LogoutCommand, LogoutResult>
    {
        public async Task<LogoutResult> Handle(LogoutCommand request, CancellationToken ct)
        {
            if (!string.IsNullOrWhiteSpace(request.RefreshTokenPlain))
            {
                var hash = RefreshTokenUtil.Hash(request.RefreshTokenPlain);
                var rt = await db.Set<RefreshToken>().FirstOrDefaultAsync(x => x.TokenHash == hash, ct);

                if (rt is not null && rt.RevokedAtUtc is null)
                {
                    rt.RevokedAtUtc = DateTime.Now;
                    rt.RevokedByIp = request.RequestIp;
                    await db.SaveChangesAsync(ct);
                }
            }

            return new LogoutResult(); // "Logged out"
        }
    }
}
