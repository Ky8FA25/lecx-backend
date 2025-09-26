namespace LecX.Domain.Entities
{
    public class Score
    {
        public int ScoreID { get; set; }
        public string StudentID { get; set; } // Corresponds to AspNetUsers(id)
        public int TestID { get; set; } // Corresponds to Test(testID)
        public DateTime DoTestAt { get; set; }
        public double ScoreValue { get; set; } // Score value
        public int NumberOfAttempt {  get; set; }

        // Navigation properties
        public virtual User Student { get; set; }
        public virtual Test Test { get; set; }
    }
}