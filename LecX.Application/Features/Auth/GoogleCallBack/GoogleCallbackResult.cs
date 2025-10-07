using LecX.Application.Features.Auth.Common;

namespace LecX.Application.Features.Auth.GoogleCallBack
{
    public sealed record GoogleCallbackResult(
        string Jwt,
        string RefreshTokenPlain,
        DateTime RefreshTokenExpiresUtc,
        UserDto User,
        IEnumerable<string> Roles
    );
}