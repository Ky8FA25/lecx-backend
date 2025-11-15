using LecX.Application.Features.StudentCourses.Common;

namespace LecX.Application.Features.Tests.Common
{
    public class TestScoreDTO
    {
        public int TestScoreId { get; set; }
        public string StudentId { get; set; }
        public int TestId { get; set; }
        public DateTime DoTestAt { get; set; }
        public double ScoreValue { get; set; } 
        public int NumberOfAttempt { get; set; }

        // Navigation properties
        public virtual StudentDTO Student { get; set; }
        public virtual TestDTO Test { get; set; }
    }
}
