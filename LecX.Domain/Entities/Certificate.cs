namespace LecX.Domain.Entities
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime CompletionDate { get; set; } = DateTime.Now;
        public string CertificateLink { get; set; }

        // Navigation properties
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
    }
}