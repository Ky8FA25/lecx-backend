using MediatR;

namespace LecX.Application.Features.Users.UserChangePassword
{
    public class UserChangePasswordRequest : IRequest<UserChangePasswordResponse>
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}