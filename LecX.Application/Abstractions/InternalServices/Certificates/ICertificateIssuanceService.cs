using LecX.Domain.Entities;

namespace LecX.Application.Abstractions.InternalServices.Certificates
{
    public interface ICertificateIssuanceService
    {
        Task<Certificate?> IssueAsync(
            string studentId,
            int courseId,
            CancellationToken ct = default);
    }
}
