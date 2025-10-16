using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.Common
{
    public sealed class PaymentResult
    {
        public string Message { get; set; } = default!;
        public int OrderCode { get; set; }
        public string Status { get; set; } = default!;
        public string? Description { get; set; }        
    }
}
