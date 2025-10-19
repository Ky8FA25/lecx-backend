using LecX.Application.Abstractions.ExternalServices.Pdf;

namespace LecX.Infrastructure.ExternalServices.Pdf
{
    public sealed class CertificatePdfService : IPdfService
    {
        public byte[] GenerateCertificate(string fullName, string courseName, DateTime issueDate)
        {
            //var doc = new CertificateDocument(fullName, courseName, issueDate);
            //using var stream = new MemoryStream();
            //doc.GeneratePdf(stream);
            //return stream.ToArray();
            throw new NotImplementedException();
        }
    }
}
