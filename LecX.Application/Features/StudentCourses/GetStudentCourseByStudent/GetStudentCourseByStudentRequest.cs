using MediatR;

namespace LecX.Application.Features.StudentCourses.GetStudentCourseByStudent
{
    public class GetStudentCourseByStudentRequest : IRequest<GetStudentCourseByStudentResponse>
    {
        public string StudentId { get; set; }
        public int CourseId { get; set; }
    }
}