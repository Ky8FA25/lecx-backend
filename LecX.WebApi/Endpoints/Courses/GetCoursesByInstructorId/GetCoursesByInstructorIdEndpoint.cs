using FastEndpoints;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Application.Features.Courses.GetCoursesByInstructorId;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Courses.GetCoursesByInstructorId
{
    public sealed class GetCoursesByInstructorIdEndpoint(ISender sender, IHttpContextAccessor httpContext)
       : Endpoint<GetCoursesByInstructorIdRequest, GetCoursesByInstructorIdResponse>
    {
        public override void Configure()
        {
            Get("/api/courses/instructor/{instructorId?}");
            Summary(s => s.Summary = "Get courses by instructor ID (paginated)");
            Description(d => d.WithTags("Courses"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetCoursesByInstructorIdRequest req, CancellationToken ct)
        {
            // Nếu instructorId không được truyền từ route/query, lấy từ JWT token (nếu đã đăng nhập)
            if (string.IsNullOrWhiteSpace(req.InstructorId))
            {
                var userId = httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    req.InstructorId = userId;
                }
                else
                {
                    await SendAsync(
                        new GetCoursesByInstructorIdResponse(new PaginatedResponse<CourseDto>(1, 10, 0, new List<CourseDto>())),
                        StatusCodes.Status400BadRequest,
                        ct);
                    return;
                }
            }

            var result = await sender.Send(req, ct);
            await SendOkAsync(result, ct);
        }
    }
}

