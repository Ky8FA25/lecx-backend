using FastEndpoints;
using LecX.Application.Features.Courses.UpdateCourse;
using MediatR;

namespace LecX.WebApi.Endpoints.Courses.UpdateCourse
{
    public sealed class UpdateCourseEndpoint(ISender sender)
        : Endpoint<UpdateCourseRequest, UpdateCourseResponse>
    {
        public override void Configure()
        {
            Put("/api/courses/{CourseId:int}");
            Summary(s => s.Summary = "Update an existing course");
            Description(d => d.WithTags("Courses"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateCourseRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);
            await SendOkAsync(result, ct);
        }
    }
}
