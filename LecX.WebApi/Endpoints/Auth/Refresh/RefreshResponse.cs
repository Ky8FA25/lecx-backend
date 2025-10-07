using LecX.Application.Features.Auth.Common;

namespace LecX.WebApi.Endpoints.Auth.Refresh
{
    public sealed class RefreshResponse
    {
        public string AccessToken { get; set; } = default!;
        public DateTime AccessTokenExpiresUtc { get; set; }
        public UserDto User { get; set; } = default!;
        public string? ReturnUrl { get; set; }
    }
}
