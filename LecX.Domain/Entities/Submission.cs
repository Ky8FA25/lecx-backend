namespace LecX.Domain.Entities
{
    public class Submission
    {
        public int SubmissionID { get; set; }
        public int AssignmentID { get; set; }
        public string StudentID { get; set; }
        public string SubmissionLink { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public virtual Assignment Assignment { get; set; }
        public virtual User Student { get; set; }
        public string FileName { get; set; }
    }
}