using LecX.Domain.Enums;

namespace LecX.Application.Features.Users.GetUserProfile
{
    public class GetUserProfileResponse
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? Address { get; set; }
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public double? WalletUser { get; set; } = 0.0;
        public string Email { get; set; }
    }
}