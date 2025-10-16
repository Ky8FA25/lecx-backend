using LecX.Application.Common.Pagination;
using LecX.Application.Features.Courses.CourseDtos;

namespace LecX.Application.Features.Courses.GetAllCourses
{
    public sealed record GetAllCoursesResponse(PaginatedResponse<CourseDto> Courses)
    {
    }
}
