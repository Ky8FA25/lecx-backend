using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.CreatePayment
{
    public sealed record CreatePaymentResponse(
         string CheckoutUrl,
         int OrderCode,
         string Message
     );
}
