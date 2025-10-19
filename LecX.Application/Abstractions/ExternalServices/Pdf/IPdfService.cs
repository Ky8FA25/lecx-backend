namespace LecX.Application.Abstractions.ExternalServices.Pdf
{
    public interface IPdfService
    {
        byte[] GenerateCertificate(string fullName, string courseName, DateTime issueDate);
    }
}
