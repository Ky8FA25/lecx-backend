using LecX.Domain.Enums;
using MediatR;

namespace LecX.Application.Features.StudentCourses.UpdateStudentCourse
{
    public sealed class UpdateStudentCourseRequest : IRequest<UpdateStudentCourseResponse>
    {
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal? Progress { get; set; }
        public CertificateStatus? CertificateStatus { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}