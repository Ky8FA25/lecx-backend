using LecX.Application.Abstractions.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Domain.Entities;
using AutoMapper;
using LecX.Application.Features.AssignmentScores.Common;

namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreById
{
    public sealed class GetAssignmentScoreByIdHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetAssignmentScoreByIdRequest, GetAssignmentScoreByIdResponse>
    {
        public async Task<GetAssignmentScoreByIdResponse> Handle(GetAssignmentScoreByIdRequest req, CancellationToken ct)
        {
            var assignmentScore = await db.Set<AssignmentScore>()
                .FindAsync(new object?[] { req.AssignmentScoreId }, ct);
            if (assignmentScore is null)
            {
                return new GetAssignmentScoreByIdResponse(false, "Assignment Score not found", null);
            }
            var assignmentScoreDto = mapper.Map<AssignmentScoreDto>(assignmentScore);
            return new GetAssignmentScoreByIdResponse(true, "Assignment Score retrieved successfully", assignmentScoreDto);
        }

    }
}
