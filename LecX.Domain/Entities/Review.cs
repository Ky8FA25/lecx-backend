namespace LecX.Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public virtual Course Course { get; set; }
        public virtual User Student { get; set; }
    }
}