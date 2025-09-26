using LecX.Domain.Enums;

namespace LecX.Domain.Entities
{
    public class StudentCourse
    {
        public int StudentCourseID { get; set; }
        public string StudentID { get; set; }
        public int CourseID { get; set; }
        public decimal Progress { get; set; }
        public CertificateStatus CertificateStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
    }
}