using LecX.Application.Features.Courses.CourseDtos;
using LecX.Domain.Enums;

namespace LecX.Application.Features.StudentCourses.Common
{
    public class StudentCourseDTO
    {
        public int StudentCourseId { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Progress { get; set; }
        public CertificateStatus CertificateStatus { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; }
        public virtual StudentDTO Student { get; set; }
        public virtual CourseDto Course { get; set; }
    }
}
