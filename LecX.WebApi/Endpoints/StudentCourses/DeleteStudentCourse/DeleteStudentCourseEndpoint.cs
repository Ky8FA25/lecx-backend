using FastEndpoints;
using LecX.Application.Features.StudentCourses.DeleteStudentCourse;
using MediatR;

namespace LecX.WebApi.Endpoints.StudentCourses.DeleteStudentCourse
{
    public class DeleteStudentCourseEndpoint(ISender sender) : EndpointWithoutRequest<DeleteStudentCourseResponse>
    {
        public override void Configure()
        {
            Delete("/api/student-courses/{studentCourseId}");
            Summary(s => s.Summary = "Delete a course enrollment for a user(only for Admin)");
            Roles("Admin");
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var studentCourseId = Route<int>("studentCourseId");
            var response = await sender.Send(new DeleteStudentCourseRequest(studentCourseId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
