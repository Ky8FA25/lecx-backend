using LecX.Application.Abstractions.ExternalServices.GoogleStorage;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Execption;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LecX.Application.Features.Certificates.DeleteCertificate
{
    public sealed class DeleteCertificateHandler(
         IAppDbContext db,
         IGoogleStorageService storage,
         ILogger<DeleteCertificateHandler> logger
     ) : IRequestHandler<DeleteCertificateRequest, Unit>
    {
        public async Task<Unit> Handle(DeleteCertificateRequest req, CancellationToken ct)
        {
            // 1) Tìm certificate
            var cert = await db.Set<Certificate>()
                .FirstOrDefaultAsync(c =>
                    c.StudentId == req.StudentId &&
                    c.CourseId == req.CourseId, ct);

            if (cert is null)
                throw new NotFoundException($"Certificate not found");

            // 2) Lấy objectName để xóa trên storage (hỗ trợ cả URL lẫn objectName thuần)
            var certId = cert.CertificateId.ToString();
            var objectName = cert.CertificateLink;

            // 3) Xóa bản ghi trong DB trước
            db.Set<Certificate>().Remove(cert);
            await db.SaveChangesAsync(ct);

            // 4) Thử xóa file trên storage (nếu có)
            if (!string.IsNullOrWhiteSpace(objectName))
            {
                try
                {
                    await storage.DeleteAsync(objectName, ct);
                }
                catch (Exception ex)
                {
                    // Không rollback DB; chỉ log cảnh báo để kiểm tra thủ công nếu cần
                    logger.LogWarning(ex,
                        "Failed to delete certificate file from storage. CertificateId={CertificateId}, ObjectName={ObjectName}",
                        certId, objectName);
                }
            }
            return Unit.Value;
        }
    }
}
