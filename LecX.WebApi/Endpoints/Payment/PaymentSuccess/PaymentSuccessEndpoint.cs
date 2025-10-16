using FastEndpoints;
using LecX.Application.Abstractions;
using LecX.Application.Features.Payment.PaymentSuccess;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Net.payOS;

namespace LecX.WebApi.Endpoints.Payment.PaymentSuccess
{
    public sealed class PaymentSuccessEndpoint(ISender sender) : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/api/payments/success");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var orderCodeStr = HttpContext.Request.Query["orderCode"].ToString();

            if (!int.TryParse(orderCodeStr, out var orderCode))
            {
                await SendAsync(new { message = "Invalid order code" }, 400, ct);
                return;
            }

            try
            {
                var result = await sender.Send(new PaymentSuccessCommand { OrderCode = orderCode }, ct);
                await SendOkAsync(result, ct);
            }
            catch (Exception ex)
            {
                await SendAsync(new { message = "Verification failed", error = ex.Message }, 500, ct);
            }
        }
    }
}
