using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.GetPaymentByStudentId
{
    public sealed class GetPaymentByStudentIdRequest : IRequest<GetPaymentByStudentIdResponse>
    {
        public string StudentId { get; set; } = default!;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
