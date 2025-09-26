namespace LecX.Domain.Entities
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int CourseID { get; set; }
        public string StudentID { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual User Student { get; set; }
    }
}