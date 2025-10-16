using LecX.Application.Abstractions;
using LecX.Application.Features.Payment.Common;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.PaymentCancel
{
    public sealed class PaymentCancelCommandHandler : IRequestHandler<PaymentCancelCommand, PaymentResult>
    {
        private readonly PayOS _payOs;
        private readonly IAppDbContext _db;

        public PaymentCancelCommandHandler(PayOS payOs, IAppDbContext db)
        {
            _payOs = payOs;
            _db = db;
        }

        public async Task<PaymentResult> Handle(PaymentCancelCommand request, CancellationToken ct)
        {
            var info = await _payOs.getPaymentLinkInformation(request.OrderCode);

            var payment = await _db.Set<LecX.Domain.Entities.Payment>()
                .FirstOrDefaultAsync(p => p.OrderCode == request.OrderCode, ct)
                ?? throw new Exception("Payment not found");

            if (info.status == "CANCELLED" || info.status == "FAILED")
            {
                payment.Status = PaymentStatus.Failed;
                payment.PaymentDate = DateTime.Now;
                await _db.SaveChangesAsync(ct);
            }

            var description = info.transactions?.FirstOrDefault()?.description ?? "Payment was cancelled";

            return new PaymentResult
            {
                Message = "Payment cancelled",
                OrderCode = request.OrderCode,
                Status = info.status,
                Description = description
            };
        }
    }
}
