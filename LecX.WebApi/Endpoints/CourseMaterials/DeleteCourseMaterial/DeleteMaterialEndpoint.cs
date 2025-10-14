using FastEndpoints;
using LecX.Application.Features.CourseMaterials.DeleteCourseMaterial;
using MediatR;

public class DeleteMaterialEndpoint(ISender sender)
    : EndpointWithoutRequest<DeleteMaterialResponse>
{
    public override void Configure()
    {
        Delete("/api/course-materials/{materialId}");

        Summary(s =>
        {
            s.Summary = "Delete a course material by ID";
        });
        Roles("Admin", "Instructor");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var materialId = Route<int>("materialId");
        var response = await sender.Send(new DeleteMaterialRequest(materialId), ct);
        await SendAsync(response, cancellation: ct);
    }
}