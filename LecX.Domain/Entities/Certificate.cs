namespace LecX.Domain.Entities
{
    public class Certificate
    {
        public int CertificateID { get; set; }
        public string StudentID { get; set; }
        public int CourseID { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CertificateLink { get; set; }

        // Navigation properties
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
    }
}