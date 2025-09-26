namespace LecX.Domain.Entities
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public virtual Course Course { get; set; }
        public string AssignmentLink { get; set; }

        public virtual IEnumerable<AssignmentScore> AssignmentScores { get; set; }
        public virtual IEnumerable<Submission> Submissions { get; set; }
    }
}