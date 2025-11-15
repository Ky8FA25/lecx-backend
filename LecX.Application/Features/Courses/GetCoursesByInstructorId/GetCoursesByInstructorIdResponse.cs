using LecX.Application.Common.Dtos;
using LecX.Application.Features.Courses.CourseDtos;

namespace LecX.Application.Features.Courses.GetCoursesByInstructorId
{
    public sealed record GetCoursesByInstructorIdResponse(PaginatedResponse<CourseDto> Data);
}


