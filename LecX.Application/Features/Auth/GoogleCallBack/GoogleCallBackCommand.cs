using LecX.Application.Features.Auth.Common;
using MediatR;

namespace LecX.Application.Features.Auth.GoogleCallBack
{
    public sealed record GoogleCallbackCommand(
        string? RequestIp
    ) : IRequest<AuthResult>;
}
