using LecX.Domain.Entities;
using LecX.Domain.Enums;

namespace LecX.Application.Common.Dtos
{
    public class UserODataDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public string? Role { get; set; }
    }

    public class CourseODataDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
        public string CoverImagePath { get; set; }
        public string InstructorId { get; set; }
        public string InstructorName { get; set; }
        public int NumberOfStudents { get; set; } = 0;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }
        public bool IsBaned { get; set; } = false;
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Rating { get; set; }
        public int NumberOfRate { get; set; } = 0;
    }

    public class CategoryODataDto
    {
        public int CategoryId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }

    public class StudentCourseODataDto
    {
        public int StudentCourseId { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Progress { get; set; }
        public CertificateStatus CertificateStatus { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
    }

    public class PaymentODataDto
    {
        public int PaymentId { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public int OrderCode { get; set; }
        public string? GatewayTransactionId { get; set; }
        public string? CheckoutUrl { get; set; }
        public string? Description { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
    }

    public class ReportODataDto
    {
        public int ReportId { get; set; }
        public string UserId { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }
        public DateTime FeedbackDate { get; set; } = DateTime.Now;
        public string UserName { get; set; }
    }

    public class InstructorConfirmationODataDto
    {
        public int ConfirmationId { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string Certificatelink { get; set; }
        public string UserName { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
