using FastEndpoints;
using LecX.Application.Features.CourseMaterials.DeleteCourseMaterial;
using MediatR;

public class DeleteMaterialEndpoint(ISender sender)
    : Endpoint<int,DeleteCourseResponse>
{
    public override void Configure()
    {
        Delete("/api/course-materials/{materialId}");

        Summary(s =>
        {
            s.Summary = "Delete a course material by ID";
            s.Params["materialId"] = "Material ID to delete";
        });

        Description(d =>
        {
            d.WithTags("CourseMaterials");
            d.Produces<DeleteCourseResponse>(200);
            d.ProducesProblem(404);
        });

        Roles("Admin", "Instructor");
    }

    public override async Task HandleAsync(int req,CancellationToken ct)
    {
        //var materialId = Route<int>("materialId");
        var response = await sender.Send(new DeleteMaterialRequest(req), ct);
        await SendAsync(response, cancellation: ct);
    }
}