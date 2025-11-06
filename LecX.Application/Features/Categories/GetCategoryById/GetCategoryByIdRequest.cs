using MediatR;

namespace LecX.Application.Features.Categories.GetCategoryById
{
    public sealed class GetCategoryByIdRequest : IRequest<GetCategoryByIdResponse>
    {
        public int CategoryId { get; set; }
    }
}