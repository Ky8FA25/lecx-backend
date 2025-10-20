using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.CreateLecture;
using LecX.Application.Features.Users.UserChangePassword;

namespace LecX.WebApi.Endpoints.Users.UserChangePassword
{
    public class UserChangePasswordValidator : Validator<UserChangePasswordRequest>
    {
        public UserChangePasswordValidator() 
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("New password is required.")
                .MinimumLength(6)
                .WithMessage("New password must be at least 6 characters long.")
                .Matches(@"[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("New password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("New password must contain at least one number.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required.")
                .Equal(x => x.NewPassword)
                .WithMessage("Confirm password must match the new password.");

        }
    }
}
