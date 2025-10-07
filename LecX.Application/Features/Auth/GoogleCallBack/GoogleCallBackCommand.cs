using MediatR;

namespace LecX.Application.Features.Auth.GoogleCallBack
{
    public sealed record GoogleCallbackCommand(
        string Provider,           // "Google"
        string ProviderKey,        // sub
        string Email,
        string? FullName,
        string? FirstName,
        string? LastName,
        string? AvatarUrl,
        string? ClientIp
    ) : IRequest<GoogleCallbackResult>;
}
