using LecX.Application.Common.Dtos;
using LecX.Application.Features.StudentCourses.Common;

namespace LecX.Application.Features.StudentCourses.GetStudentsFilteredByCourse
{
    public class GetStudentsFilteredByCourseResponse : GenericResponseClass<PaginatedResponse<StudentCourseDTO>>;
}