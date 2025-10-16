using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Payment.PaymentDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.GetPaymentByStudentId
{
    public sealed class GetPaymentByStudentIdHandler(IAppDbContext db, IMapper mapper)
     : IRequestHandler<GetPaymentByStudentIdRequest, GetPaymentByStudentIdResponse>
    {
        public async Task<GetPaymentByStudentIdResponse> Handle(GetPaymentByStudentIdRequest request, CancellationToken cancellationToken)
        {
            var query = db.Set<LecX.Domain.Entities.Payment>()
                .Include(p => p.Course)
                .Where(p => p.StudentId == request.StudentId)
                .OrderByDescending(p => p.PaymentDate);

            var paginated = await PaginatedResponse<LecX.Domain.Entities.Payment>.CreateAsync(
                query, request.PageIndex, request.PageSize, cancellationToken);

            var mapped = paginated.MapItems(payment => mapper.Map<PaymentDto>(payment));

            return new GetPaymentByStudentIdResponse(mapped);
        }
    }
}
