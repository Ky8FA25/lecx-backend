using MediatR;

namespace LecX.Application.Features.Payment.GetPaymentByCourseId
{
    public sealed class GetPaymentByCourseIdRequest : IRequest<GetPaymentByCourseIdResponse>
    {
        public int CourseId { get; set; }
    }
}

