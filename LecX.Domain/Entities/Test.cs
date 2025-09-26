using LecX.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LecX.Domain.Entities
{
    public class Test
    {
        public int TestId { get; set; } // Primary key with auto-increment

        [MaxLength(255)]
        public string Title { get; set; } // Title of the test

        [MaxLength(255)]
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

        public virtual Course Course { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; } = new List<Question>();
        public virtual IEnumerable<TestScore> TestScores { get; set; } = new List<TestScore>();
    }
}