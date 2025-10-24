using MediatR;

namespace LecX.Application.Features.StudentCourses.GetStudentCourseDetailById
{
    public sealed record GetStudentCourseDetailByIdRequest(
        int StudentCourseId
        ) :IRequest<GetStudentCourseDetailByIdResponse>;
}