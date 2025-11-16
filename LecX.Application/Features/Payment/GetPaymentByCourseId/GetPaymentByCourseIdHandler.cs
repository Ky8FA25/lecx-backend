using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Payment.PaymentDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Payment.GetPaymentByCourseId
{
    public sealed class GetPaymentByCourseIdHandler(IAppDbContext db, IMapper mapper)
     : IRequestHandler<GetPaymentByCourseIdRequest, GetPaymentByCourseIdResponse>
    {
        public async Task<GetPaymentByCourseIdResponse> Handle(GetPaymentByCourseIdRequest request, CancellationToken cancellationToken)
        {
            var payments = await db.Set<LecX.Domain.Entities.Payment>()
                .Include(p => p.Course)
                .Include(p => p.Student)
                .Where(p => p.CourseId == request.CourseId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync(cancellationToken);

            var mapped = payments.Select(payment => mapper.Map<PaymentDto>(payment)).ToList();

            return new GetPaymentByCourseIdResponse(mapped);
        }
    }
}

