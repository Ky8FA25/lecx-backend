using LecX.Application.Features.Courses.CreateCourse;
using FastEndpoints;
using MediatR;

namespace LecX.WebApi.Endpoints.Courses.CreateCourse
{
    public sealed class CreateCourseEndpoint(ISender sender)
        : Endpoint<CreateCourseRequest, CreateCourseResponse>
    {
        public override void Configure()
        {
            Post("/api/courses/create");
            Summary(s => s.Summary = "Create a new course");
            Description(d => d.WithTags("Courses"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateCourseRequest req, CancellationToken ct)
            => await SendAsync(await sender.Send(req, ct), cancellation: ct);
    }
}
