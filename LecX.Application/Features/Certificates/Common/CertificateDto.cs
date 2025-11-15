namespace LecX.Application.Features.Certificates.Common
{
    public class CertificateDto
    {
        public int CertificateId { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CertificateLink { get; set; }
    }
}
