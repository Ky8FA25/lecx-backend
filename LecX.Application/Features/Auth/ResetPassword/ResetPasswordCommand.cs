using MediatR;

namespace LecX.Application.Features.Auth.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}