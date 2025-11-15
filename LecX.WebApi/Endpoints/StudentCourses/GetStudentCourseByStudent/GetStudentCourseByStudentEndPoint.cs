using System.Security.Claims;
using FastEndpoints;
using LecX.Application.Features.StudentCourses.GetCoursesFilteredByStudent;
using LecX.Application.Features.StudentCourses.GetStudentCourseByStudent;
using LecX.Domain.Enums;
using MediatR;

namespace LecX.WebApi.Endpoints.StudentCourses.GetStudentCourseByStudent
{
    public class GetStudentCourseByStudentEndPoint(ISender sender, IHttpContextAccessor httpContext) : Endpoint<GetStudentCourseByStudentRequest, GetStudentCourseByStudentResponse>
    {
        public override void Configure()
        {
            Get("/api/student-courses/check-exist");
            Summary(s =>
            {
                s.Summary = "Kiểm tra xem sinh viên đã mua khóa học hay chưa";
                s.Description = "Trả về true nếu sinh viên đã đăng ký khóa học, ngược lại là false.";
            });
        }

        public override async Task HandleAsync(GetStudentCourseByStudentRequest req, CancellationToken ct)
        {
            try
            {
                var studentId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(studentId))
                {
                    await SendAsync(
                        new GetStudentCourseByStudentResponse { Message = "User not found", Success = false },StatusCodes.Status400BadRequest, ct);
                    return;
                }
                req.StudentId = studentId!;
                var response = await sender.Send(req, ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new GetStudentCourseByStudentResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
