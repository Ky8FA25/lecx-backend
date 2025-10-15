using FastEndpoints;
using LecX.Application.Features.CourseMaterials.GetAllCourseMaterials;
using MediatR;

namespace LecX.WebApi.Endpoints.CourseMaterials.GetAllCourseMaterials
{
    public sealed class GetAllCourseMaterialsEndpoint(ISender sender) : EndpointWithoutRequest<GetAllMaterialResponse>
    {
        public override void Configure()
        {
            Get("/api/course-materials/course/{courseId}");
            Summary(s =>
            {
                s.Summary = "Get all course materials in a course";
            });
            Roles("Admin", "Instructor");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var courseId = Route<int>("courseId");
            var response = await sender.Send(new GetAllMaterialRequest(courseId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
