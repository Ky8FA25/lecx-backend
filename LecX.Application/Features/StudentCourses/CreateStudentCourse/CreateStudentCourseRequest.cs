using MediatR;

namespace LecX.Application.Features.StudentCourses.CreateStudentCourse
{
    public sealed record CreateStudentCourseRequest(
        string StudentId,
        int CourseId
        ) : IRequest<CreateStudentCourseResponse>;
}