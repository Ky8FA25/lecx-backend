using FastEndpoints;
using LecX.Application.Abstractions;
using LecX.Application.Features.Payment.PaymentCancel;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Net.payOS;

namespace LecX.WebApi.Endpoints.Payment.PaymentCancel
{
    public sealed class PaymentCancelEndpoint(ISender sender) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/api/payments/cancel");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var orderCodeStr = HttpContext.Request.Query["orderCode"].ToString();

            if (!int.TryParse(orderCodeStr, out var oderCode))
            {
                await SendAsync(new { message = "Invalid order code" }, 400, ct);
            }

            try
            {
                var result = await sender.Send(new PaymentCancelCommand { OrderCode = oderCode }, ct);

                await SendOkAsync(result, ct);
            }
            catch (Exception ex)
            {
                await SendAsync(new { message = "Cancellation failed", error = ex.Message }, 500, ct);
            }
        }
    }
}
