using LecX.Application.Features.Payment.PaymentDtos;

namespace LecX.Application.Features.Payment.GetPaymentByCourseId
{
    public sealed record GetPaymentByCourseIdResponse(
     List<PaymentDto> Payments);
}

