using LecX.Domain.Enums;

namespace LecX.Domain.Entities
{
    public class StudentCourse
    {
        public int StudentCourseId { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Progress { get; set; }
        public CertificateStatus CertificateStatus { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; }
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
    }
}