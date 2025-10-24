using FastEndpoints;
using LecX.Application.Features.StudentCourses.GetCoursesFilteredByStudent;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.StudentCourses.GetCoursesFilteredByStudent
{
    public class GetCoursesFilteredByStudentEndpoint(ISender sender, IHttpContextAccessor httpContext) : Endpoint<GetCoursesFilteredByStudentRequest, GetCoursesFilteredByStudentResponse>
    {
        public override void Configure()
        {
            Get("/api/student-courses/courses");
            Summary(s => s.Summary = "Get courses of user with filtering (keyword, category, status)");
        }
        public override async Task HandleAsync(GetCoursesFilteredByStudentRequest req, CancellationToken ct)
        {
            try
            {
                var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    await SendAsync(
                        new GetCoursesFilteredByStudentResponse { Message = "UserId not found", Success = false }, StatusCodes.Status400BadRequest, ct);
                    return;
                }
                req.StudentId = userId!;
                var response = await sender.Send(req, ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new GetCoursesFilteredByStudentResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
