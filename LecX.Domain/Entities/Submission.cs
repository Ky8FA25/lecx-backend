namespace LecX.Domain.Entities
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
        public string SubmissionLink { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public virtual Assignment Assignment { get; set; }
        public virtual User Student { get; set; }
        public string FileName { get; set; }
    }
}