using FastEndpoints;
using LecX.Application.Features.CourseMaterials.UpdateCourseMaterial;
using MediatR;

namespace LecX.WebApi.Endpoints.CourseMaterials.UpdateCourseMaterial
{
    public sealed class UpdateCourseMaterialEndpoint(ISender sender) : Endpoint<UpdateMaterialRequest,UpdateMaterialResponse>
    {
        public override void Configure()
        {
            Patch("/api/course-materials");
            Summary(s => s.Summary = "Update a material for a course");
            Roles("Admin", "Instructor");
        }
        public override async Task HandleAsync(UpdateMaterialRequest req, CancellationToken ct)
            => await SendAsync(await sender.Send(req, ct), cancellation: ct);
    }
}
