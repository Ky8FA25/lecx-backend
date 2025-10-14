using FastEndpoints;

namespace LecX.WebApi.Endpoints.Auth.ConfirmEmail
{
    public sealed class ConfirmEmailRequest
    {
        [QueryParam]
        public string UserId { get; init; } = default!;

        [QueryParam]
        public string Token { get; init; } = default!;

        [QueryParam]
        public string? ReturnUrl { get; init; }
    }
}