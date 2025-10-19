namespace LecX.Application.Abstractions.ExternalServices.Pdf
{
    public interface IPdfService
    {
        Task<Stream> GenerateCertificateAsync(
              string studentName,
              string courseName,
              string completionDate,
              string instructorName,
              string instructorTitle,
              string verifyUrl);
    }
}