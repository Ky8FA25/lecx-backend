using FastEndpoints;
using LecX.Application.Features.Certificates.DeleteCertificate;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Certificates.DeleteCertificate
{
    public class DeleteCertificateEndpoint(
        ISender sender,
        IHttpContextAccessor httpContext
        ) : Endpoint<DeleteCertificateRequest>
    {
        public override void Configure()
        {
            Delete("api/certificates/{courseId:int}");
            Summary(s =>
            {
                s.Summary = "Delete certificate by courseId & studentId (For test only)";
            });
        }
        public override async Task HandleAsync(DeleteCertificateRequest req, CancellationToken ct)
        {
            var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            req.StudentId = userId;
            await sender.Send(req, ct);
            await SendNoContentAsync(ct);
        }
    }
}