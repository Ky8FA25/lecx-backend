namespace LecX.Application.Features.StudentCourses.GetStudentCourseByStudent
{
    public class GetStudentCourseByStudentResponse
    {
        public bool IsEnrolled { get; set; }
        public decimal? Progress { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}