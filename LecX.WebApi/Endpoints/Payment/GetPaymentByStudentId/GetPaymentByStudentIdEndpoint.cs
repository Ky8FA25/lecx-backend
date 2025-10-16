using FastEndpoints;
using LecX.Application.Features.Payment.GetPaymentByStudentId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Payment.GetPaymentByStudentId
{
    [Authorize]
    public class GetPaymentByStudentIdEndpoint(ISender sender)
    : Endpoint<GetPaymentByStudentIdRequest, GetPaymentByStudentIdResponse>
    {
        public override void Configure()
        {
            Get("/api/payments/{StudentId}");
            Summary(s => s.Summary = "Get paginated payments by student ID");
            Description(d => d.WithTags("Payments"));
            AllowAnonymous(); // Nếu cần
        }

        public override async Task HandleAsync(GetPaymentByStudentIdRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);
            await SendOkAsync(result, ct);
        }
    }
}
