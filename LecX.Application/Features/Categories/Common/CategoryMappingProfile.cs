using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LecX.Application.Features.Comments.Common;
using LecX.Application.Features.Comments.CreateComment;
using LecX.Domain.Entities;

namespace LecX.Application.Features.Categories.CategoryDtos
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>();           
        }
    }
}
