using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Abstractions;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Application.Features.Courses.CreateCourse;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LecX.Application.Features.Users.GetUserProfile
{
    public class GetUserProfileHandler(UserManager<User> userManager) : IRequestHandler<GetUserProfileRequest, GetUserProfileResponse>
    {
        public async Task<GetUserProfileResponse> Handle(GetUserProfileRequest request, CancellationToken cancellationToken)
        {
            var userPrincipal = request.ClaimsPrincipal;

            if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập");

            var userId = userManager.GetUserId(userPrincipal);
            if (userId == null)
                throw new UnauthorizedAccessException("Không thể xác định ID người dùng");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("Không tìm thấy người dùng");

            return new GetUserProfileResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                ProfileImagePath = user.ProfileImagePath,
                Dob = user.Dob,
                Gender = user.Gender,
                WalletUser = user.WalletUser
            };
        }
    }
}
