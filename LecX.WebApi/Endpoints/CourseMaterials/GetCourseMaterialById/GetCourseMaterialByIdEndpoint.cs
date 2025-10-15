using FastEndpoints;
using LecX.Application.Features.CourseMaterials.GetCourseMaterialById;
using MediatR;

namespace LecX.WebApi.Endpoints.CourseMaterials.GetCourseMaterialById
{
    public class GetCourseMaterialByIdEndpoint(ISender sender): EndpointWithoutRequest<GetMaterialByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/course-materials/{materialId}");
            Summary(s =>
            {
                s.Summary = "Get course material by material id";
            });
            Roles("Admin", "Instructor");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var materialId = Route<int>("materialId");
            var response = await sender.Send(new GetMaterialByIdRequest(materialId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
