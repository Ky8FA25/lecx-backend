using LecX.Application.Common.Dtos;
using LecX.Application.Features.StudentCourses.Common;

namespace LecX.Application.Features.StudentCourses.GetCoursesFilteredByStudent
{
    public sealed class GetCoursesFilteredByStudentResponse : GenericResponseClass<PaginatedResponse<StudentCourseDTO>>;
}