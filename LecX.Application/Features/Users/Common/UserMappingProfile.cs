using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LecX.Application.Features.Categories.CategoryDtos;
using LecX.Application.Features.Users.GetUserProfile;
using LecX.Domain.Entities;

namespace LecX.Application.Features.Users.Common
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<User, GetUserProfileRequest>();
        }
    }
}
