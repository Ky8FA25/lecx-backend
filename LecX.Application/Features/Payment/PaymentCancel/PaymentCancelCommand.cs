using LecX.Application.Features.Payment.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.PaymentCancel
{
    public sealed class PaymentCancelCommand : IRequest<PaymentResult>
    {
        public int OrderCode { get; set; }
    }
}
