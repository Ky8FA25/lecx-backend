using LecX.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LecX.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public Gender? Gender { get; set; }
        public double? WalletUser { get; set; } = 0.0;
    }
}