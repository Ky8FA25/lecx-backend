using FastEndpoints;
using LecX.Application.Features.Payment;
using LecX.Application.Features.Payment.CreatePayment;
using LecX.Domain.Enums;
using MediatR;

namespace LecX.WebApi.Endpoints.Payment.CreatePayment
{
    public sealed class CreatePaymentEndpoint(ISender sender)
       : Endpoint<CreatePaymentRequest, CreatePaymentResponse>
    {
        public override void Configure()
        {
            Post("/api/payments/create");
            Summary(s => s.Summary = "Create PayOS payment link for a course");
            Roles("Student");
        }

        public override async Task HandleAsync(CreatePaymentRequest req, CancellationToken ct)
        {
            var res = await sender.Send(req, ct);
            await SendOkAsync(res, ct);
        }
    }
}
