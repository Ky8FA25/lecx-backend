namespace LecX.Domain.Enums
{
    /// <summary>
    /// roles for users in the system
    /// </summary>
    public enum Role
    {
        Admin,
        Instructor,
        Student
    }

    /// <summary>
    /// Course categories for classification
    /// </summary>
    public enum CourseCategory
    {
        Development,
        Business,
        ITAndSoftware,
        Design,
        Marketing,
        PersonalDevelopment,
        HealthAndFitness,
        Music,
        Photography,
        LanguageLearning,
        TeachingAndAcademics,
        Other
    }

    /// <summary>
    /// Course levels to indicate difficulty
    /// </summary>
    public enum CourseLevel
    {
        Beginner,
        Intermediate,
        Advanced
    }

    /// <summary>
    /// Payment status for transactions
    /// </summary>
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

    /// <summary>
    /// Course status to indicate its availability
    /// </summary>
    public enum CourseStatus
    {
        Draft,
        Published,
        Archived
    }

    /// <summary>
    /// Test status to track progress
    /// </summary>
    public enum TestStatus
    {
        Active,
        Inactive,
        Completed
    }

    /// <summary>
    /// Certificate status for course completion
    /// </summary>
    public enum CertificateStatus
    {
        Pending,
        Completed
    }

    /// <summary>
    /// File types for uploaded content
    /// </summary>
    public enum FileType
    {
        Image,
        Video,
        Document,
        Other
    }
}
