using LecX.Application.Common.Dtos;
using LecX.Application.Features.Payment.PaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.GetPaymentByStudentId
{
    public sealed record GetPaymentByStudentIdResponse(
     PaginatedResponse<PaymentDto> Payments);


}
