using FastEndpoints;
using LecX.Application.Features.Courses.GetAllCourses;
using MediatR;

namespace LecX.WebApi.Endpoints.Courses.GetAllCourses
{
    public sealed class GetAllCoursesEndpoint(ISender sender)
        : Endpoint<GetAllCoursesRequest, GetAllCoursesResponse>
    {
        public override void Configure()
        {
            Get("/api/courses/all");
            Summary(s => s.Summary = "Get all courses (paginated)");
            Description(d => d.WithTags("Courses"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAllCoursesRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);
            await SendOkAsync(result, ct);
        }
    }
}
