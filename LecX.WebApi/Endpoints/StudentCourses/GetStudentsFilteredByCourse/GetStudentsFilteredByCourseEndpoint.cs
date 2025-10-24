using FastEndpoints;
using LecX.Application.Features.StudentCourses.GetStudentsFilteredByCourse;
using MediatR;

namespace LecX.WebApi.Endpoints.StudentCourses.GetStudentsFilteredByCourse
{
    public class GetStudentsFilteredByCourseEndpoint(ISender sender) : Endpoint<GetStudentsFilteredByCourseRequest,GetStudentsFilteredByCourseResponse>
    {
        public override void Configure()
        {
            Get("/api/student-courses/students");
            Summary(s => s.Summary = "Get students of a course with filtering (keyword, status)");
        }
        public override async Task HandleAsync(GetStudentsFilteredByCourseRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
