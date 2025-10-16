using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.PaymentDtos
{
    public sealed record PaymentDto(
         int PaymentId,
         int CourseId,
         string CourseTitle,
         decimal Amount,
         string Status,
         DateTime PaymentDate,
         int OrderCode,
         string? CheckoutUrl,
         string? Description
     );
}
