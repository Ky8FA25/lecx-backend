namespace LecX.Domain.Entities
{
    public class TestScore
    {
        public int TestScoreId { get; set; }
        public string StudentId { get; set; } // Corresponds to AspNetUsers(Id)
        public int TestId { get; set; } // Corresponds to Test(testId)
        public DateTime DoTestAt { get; set; }
        public double ScoreValue { get; set; } // Score value
        public int NumberOfAttempt {  get; set; }

        // Navigation properties
        public virtual User Student { get; set; }
        public virtual Test Test { get; set; }
    }
}