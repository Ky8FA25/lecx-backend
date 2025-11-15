using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Features.Users.GetUserProfile;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LecX.Application.Features.Users.UserChangePassword
{
    public class UserChangePasswordHandler(UserManager<User> userManager) : IRequestHandler<UserChangePasswordRequest, UserChangePasswordResponse>
    {

        public async Task<UserChangePasswordResponse> Handle(UserChangePasswordRequest request, CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return new UserChangePasswordResponse
                {
                    Success = false,
                    Message = "Confirm password not match!"
                };
            }

            // ✅ 2. Find user by Id
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new UserChangePasswordResponse
                {
                    Success = false,
                    Message = "User not found!"
                };
            }

            // ✅ 3. Change password
            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new UserChangePasswordResponse
                {
                    Success = false,
                    Message = $"Failed change password: {errors}"
                };
            }

            // ✅ 4. (Optional) Update SecurityStamp để logout user trên thiết bị khác
            await userManager.UpdateSecurityStampAsync(user);

            return new UserChangePasswordResponse
            {
                Success = true,
                Message = "Change password successfully!"
            };
        }
    }
    
}
