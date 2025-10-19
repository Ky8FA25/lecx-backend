using FastEndpoints;
using LecX.Application.Features.Certificates.CreateCertificate;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Certificates.CrerateCertificate
{
    public class CreateCertificateEnpoint(
        ISender sender,
        IHttpContextAccessor httpContext
        ) : Endpoint<CreateCertificateRequest, CreateCertificateResponse>
    {
        public override void Configure()
        {
            Post("api/certificates");
            Summary(s =>
            {
                s.Summary = "Create new certificate (For test only)";
            });
        }
        public override async Task HandleAsync(CreateCertificateRequest req, CancellationToken ct)
        {
            var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            req.StudentId = userId;
            var response =  await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
