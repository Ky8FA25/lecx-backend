using System.Text.Json.Serialization;
using MediatR;

namespace LecX.Application.Features.StudentCourses.GetStudentCourseByStudent
{
    public class GetStudentCourseByStudentRequest : IRequest<GetStudentCourseByStudentResponse>
    {
        [JsonIgnore]
        public string StudentId { get; set; }
        public int CourseId { get; set; }
    }
}