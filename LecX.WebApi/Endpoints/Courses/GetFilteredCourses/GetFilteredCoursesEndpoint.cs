using FastEndpoints;
using LecX.Application.Features.Courses.GetFilteredCourses;
using MediatR;

namespace LecX.WebApi.Endpoints.Courses.GetFilteredCourses
{
    public sealed class GetFilteredCoursesEndpoint(ISender sender)
       : Endpoint<GetFilteredCoursesRequest, GetFilteredCoursesResponse>
    {
        public override void Configure()
        {
            Get("/api/courses/filter");
            Summary(s => s.Summary = "Get filtered courses (paginated)");
            Description(d => d.WithTags("Courses"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetFilteredCoursesRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);
            await SendOkAsync(result, ct);
        }
    }
}
