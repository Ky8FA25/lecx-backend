using LecX.Application.Common.Dtos;
using LecX.Application.Features.Courses.CourseDtos;

namespace LecX.Application.Features.Courses.GetAllCourses
{
    public sealed record GetAllCoursesResponse(PaginatedResponse<CourseDto> Courses)
    {
    }
}
