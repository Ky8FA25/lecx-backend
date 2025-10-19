using MediatR;

namespace LecX.Application.Features.Categories.GetAllCategories
{
    public sealed class GetAllCategoriesRequest : IRequest<GetAllCategoriesResponse>
    {
        public string? ReturnUrl { get; set; }
    }
}