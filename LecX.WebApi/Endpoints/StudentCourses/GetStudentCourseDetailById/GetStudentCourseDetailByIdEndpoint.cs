using FastEndpoints;
using LecX.Application.Features.StudentCourses.GetStudentCourseDetailById;
using MediatR;

namespace LecX.WebApi.Endpoints.StudentCourses.GetStudentCourseDetailById
{
    public class GetStudentCourseDetailByIdEndpoint(ISender sender) : EndpointWithoutRequest<GetStudentCourseDetailByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/student-courses/{studentCourseId}");
            Summary(s => s.Summary = "Get course detail of a student by ID");
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var studentCourseId = Route<int>("studentCourseId");
            var response = await sender.Send(new GetStudentCourseDetailByIdRequest(studentCourseId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
