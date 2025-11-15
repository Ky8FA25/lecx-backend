using LecX.Domain.Enums;

namespace LecX.Application.Features.Tests.Common
{
    public class TestDTO
    {
        public int TestId { get; set; } // Primary key with auto-increment

        public string Title { get; set; } // Title of the test

        public string? Description { get; set; }
        public int CourseId { get; set; } // Foreign key to Courses

        public DateTime StartTime { get; set; } // Start time of the test
        public TimeSpan? TestTime { get; set; } // The Actual Time for the test
        public DateTime EndTime { get; set; } // End time of the test

        public int NumberOfQuestion { get; set; } // Number of questions in the test
        public TestStatus Status { get; set; } = TestStatus.Inactive;
        public double? PassingScore { get; set; }
        public string AlowRedo { get; set; }
        public int? NumberOfMaxAttempt { get; set; }
    }
}
