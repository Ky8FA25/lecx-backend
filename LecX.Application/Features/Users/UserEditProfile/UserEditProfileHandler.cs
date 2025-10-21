using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Categories.CategoryDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
           await userManager.UpdateAsync(user);

            return new UserEditProfileResponse
            {
                Success = true,
                Message = "Profile updated successfully."
            };
        }
    }
}
