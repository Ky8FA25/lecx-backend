using FastEndpoints;
using LecX.Application.Features.Categories.GetAllCategories;
using MediatR;

namespace LecX.WebApi.Endpoints.Categories.GetAllCategories
{
    public sealed class GetAllCategoriesEndpoint(ISender sender)
        : Endpoint<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
        public override void Configure()
        {
            Get("/api/categories/all");
            Summary(s => s.Summary = "Get all categories");
            Description(d => d.WithTags("Categories"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllCategoriesRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);
            await SendOkAsync(result, ct);
        }
    }
}
