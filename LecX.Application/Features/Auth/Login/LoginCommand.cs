using LecX.Application.Features.Auth.Common;
using MediatR;

namespace LecX.Application.Features.Auth.Login
{
    public sealed class LoginCommand : IRequest<LoginResult>
    {
        public string EmailOrUserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? ReturnUrl { get; set; }

        // đưa IP từ Endpoint vào đây (hoặc dùng Behavior)
        public string? RequestIp { get; set; }
    }
}
