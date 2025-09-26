namespace LecX.Domain.Entities
{
    public class LectureCompletion
    {
        public int CompletionId { get; set; } // Primary key with Identity
        public string StudentId { get; set; } // Foreign key to AspNetUsers
        public int LectureId { get; set; } // Foreign key to Lecture
        public DateTime? CompletionDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual User Student { get; set; }
        public virtual Lecture Lecture { get; set; }
    }
}