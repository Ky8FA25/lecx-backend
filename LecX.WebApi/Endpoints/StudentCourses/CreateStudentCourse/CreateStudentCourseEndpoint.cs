using MediatR;
using FastEndpoints;
using LecX.Application.Features.StudentCourses.CreateStudentCourse;
namespace LecX.WebApi.Endpoints.StudentCourses.CreateStudentCourse
{
    public class CreateStudentCourseEndpoint(ISender sender) : Endpoint<CreateStudentCourseRequest, CreateStudentCourseResponse>
    {
        public override void Configure()
        {
            Post("/api/student-courses");
            Summary(s => s.Summary = "Create a new course enrollment for a user(only for Admin)");
            Roles("Admin");
        }
        public override async Task HandleAsync(CreateStudentCourseRequest req, CancellationToken ct)
        {
            var response =  await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
