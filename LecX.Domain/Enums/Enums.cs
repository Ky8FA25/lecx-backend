namespace LecX.Domain.Enums
{
    /// <summary>
    /// roles for users in the system
    /// NOTE: Fixed IDs per requirement: Admin=1, Student=2, Instructor=3
    /// </summary>
    public enum Role
    {
        Admin = 1,
        Student = 2,
        Instructor = 3
    }

    /// <summary>
    /// Course categories for classification
    /// </summary>
    public enum CourseCategory
    {
        Development = 0,
        Business = 1,
        ITAndSoftware = 2,
        Design = 3,
        Marketing = 4,
        PersonalDevelopment = 5,
        HealthAndFitness = 6,
        Music = 7,
        Photography = 8,
        LanguageLearning = 9,
        TeachingAndAcademics = 10,
        Other = 11
    }

    /// <summary>
    /// Course levels to indicate difficulty
    /// </summary>
    public enum CourseLevel
    {
        Beginner = 0,
        Intermediate = 1,
        Advanced = 2
    }

    /// <summary>
    /// Payment status for transactions
    /// </summary>
    public enum PaymentStatus
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Refunded = 3
    }

    /// <summary>
    /// Course status to indicate its availability
    /// </summary>
    public enum CourseStatus
    {
        Draft = 0,
        Published = 1,
        Archived = 2
    }

    /// <summary>
    /// Test status to track progress
    /// </summary>
    public enum TestStatus
    {
        Active = 0,
        Inactive = 1,
        Completed = 2
    }

    /// <summary>
    /// Certificate status for course completion
    /// </summary>
    public enum CertificateStatus
    {
        Pending = 0,
        Completed = 1
    }

    /// <summary>
    /// File types for uploaded content
    /// </summary>
    public enum FileType
    {
        Image = 0,
        Video = 1,
        Document = 2,
        Other = 3
    }

    public enum NotificationType
    {
        Info = 0,
        Warning = 1,
        Alert = 2
    }

    public enum ReportStatus
    {
        Pending = 0,
        Reviewed = 1,
        Resolved = 2
    }

    public enum RefundStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public enum InstructorConfirmationStatus
    {
        Pending = 0,
        Confirmed = 1,
        Rejected = 2
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2
    }
}
