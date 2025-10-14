using FastEndpoints;
using LecX.Application.Features.CourseMaterials.CreateCourseMaterials;
using MediatR;

namespace LecX.WebApi.Endpoints.CourseMaterials.CreateCourseMaterial
{
    public sealed class CreateMaterialEndpoint(ISender sender)
        : Endpoint<CreateMaterialRequest, CreateMaterialResponse>
    {
        public override void Configure()
        {
            Post("/api/course-materials");
            Summary(s => s.Summary = "Create a new course material for a course");
            Roles("Admin", "Instructor");
        }
        public override async Task HandleAsync(CreateMaterialRequest req, CancellationToken ct)
            => await SendAsync(await sender.Send(req, ct), cancellation: ct);
    }
}
