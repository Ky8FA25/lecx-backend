using LecX.Application.Features.Categories.CategoryDtos;

namespace LecX.Application.Features.Categories.GetAllCategories
{
    public sealed class GetAllCategoriesResponse 
    {
        public List<CategoryDto> Categories { get; set; } = new();
    }
}