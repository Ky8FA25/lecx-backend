using AutoMapper;
using LecX.Application.Features.Categories.GetCategoryById;
using LecX.Domain.Entities;

namespace LecX.WebApi.Endpoints.Categories.Common
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryByIdResponse>();
        }
    }
}
