using MediatR;

namespace LecX.Application.Features.StudentCourses.DeleteStudentCourse
{
    public sealed record DeleteStudentCourseRequest(
        int StudentCourseId
        ) : IRequest<DeleteStudentCourseResponse>;
}