using LecX.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LecX.Application.Features.Users.UserEditProfile
{
    public class UserEditProfileRequest : IRequest<UserEditProfileResponse>
    {
        public string UserId { get; set; }
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public Gender? Gender { get; set; }

        public IFormFile? ProfileImage { get; set; }
    }
}