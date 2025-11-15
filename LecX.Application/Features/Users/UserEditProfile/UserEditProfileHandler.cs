using AutoMapper;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LecX.Application.Features.Users.UserEditProfile
{
    public class UserEditProfileHandler(UserManager<User> userManager, IMapper mapper)
       : IRequestHandler<UserEditProfileRequest, UserEditProfileResponse>
    {
        public async Task<UserEditProfileResponse> Handle(UserEditProfileRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new UserEditProfileResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Address = request.Address;
            user.Dob = request.Dob;
            user.Gender = request.Gender;
            
            //user upload image
            if (!string.IsNullOrEmpty(request.ProfileImage))
            {
                user.ProfileImagePath = request.ProfileImage;
            }

            await userManager.UpdateAsync(user);

            return new UserEditProfileResponse
            {
                Success = true,
                Message = "Profile updated successfully."
            };
        }
    }
}
