using FastEndpoints;
using LecX.Application.Features.Categories.GetAllCategories;
using LecX.Application.Features.Categories.GetCategoryById;
using MediatR;

namespace LecX.WebApi.Endpoints.Categories.GetCategoryById
{
    public sealed class GetCategoryByIdEndpoint(ISender sender)
        : Endpoint<GetCategoryByIdRequest, GetCategoryByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/categories/{categoryId}");
            Summary(s => s.Summary = "Get a category by its ID");
        }

        public override async Task HandleAsync(GetCategoryByIdRequest req, CancellationToken ct)
        {
            var categoryId = Route<int>("categoryId");
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
