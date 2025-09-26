namespace LecX.Domain.Entities
{
    public class LectureCompletion
    {
        public int CompletionID { get; set; } // Primary key with Identity
        public string StudentID { get; set; } // Foreign key to AspNetUsers
        public int LectureID { get; set; } // Foreign key to Lecture
        public DateTime? CompletionDate { get; set; } // Nullable Completion Date

        // Navigation properties
        public virtual User Student { get; set; }
        public virtual Lecture Lecture { get; set; }
    }
}