using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using MediatR;
using LecX.Application.Features.AssignmentScores.Common;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace LecX.Application.Features.AssignmentScores.CreateAssignmentScore
{
    public sealed class CreateAssignmentScoreHandler (IAppDbContext db , IMapper mapper ) : IRequestHandler<CreateAssignmentScoreRequest, CreateAssignmentScoreResponse>
    {
        public async Task<CreateAssignmentScoreResponse> Handle(CreateAssignmentScoreRequest req, CancellationToken ct)
        {
            var assignmentScore = mapper.Map<AssignmentScore>(req);
            await db.Set<AssignmentScore>().AddAsync(assignmentScore, ct);
            try
            {
                var affected = await db.SaveChangesAsync(ct);
                return affected > 0
                    ? new CreateAssignmentScoreResponse(true, "Success", mapper.Map<AssignmentScoreDto>(assignmentScore))
                    : new CreateAssignmentScoreResponse(false, "Failed", null);
            }
            catch (DbUpdateException)
            {
                return new CreateAssignmentScoreResponse(false, "Error while creating assignment score", null);
            }
        }
    }
}
