using MediatR;

namespace LecX.Application.Features.Auth.Logout
{
    public sealed class LogoutCommand : IRequest<LogoutResult>
    {
        public string? RefreshTokenPlain { get; init; }
        public string? RequestIp { get; init; }
    }
}
