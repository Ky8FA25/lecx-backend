using FastEndpoints;
using LecX.Application.Features.StudentCourses.UpdateStudentCourse;
using MediatR;

namespace LecX.WebApi.Endpoints.StudentCourses.UpdateStudentCourse
{
    public class UpdateStudentCourseEndpoint(ISender sender) : Endpoint<UpdateStudentCourseRequest, UpdateStudentCourseResponse>
    {
        public override void Configure()
        {
            Put("/api/student-courses");
            Summary(s => s.Summary = "Update a course enrollment for a user");
        }
        public override async Task HandleAsync(UpdateStudentCourseRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
