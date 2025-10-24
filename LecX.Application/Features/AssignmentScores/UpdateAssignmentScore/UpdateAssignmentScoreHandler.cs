using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LecX.Application.Features.AssignmentScores.UpdateAssignmentScore
{
    public sealed class UpdateAssignmentScoreHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<UpdateAssignmentScoreRequest, UpdateAssignmentScoreResponse>
    {
        public async Task<UpdateAssignmentScoreResponse> Handle(UpdateAssignmentScoreRequest req, CancellationToken ct)
        {
            var assignmentScore = await db.Set<AssignmentScore>()
                .SingleOrDefaultAsync( c => c.AssignmentScoreId == req.AssignmentScoreId, ct);
            if (assignmentScore is null)
                throw new KeyNotFoundException("Assignment score not found");
            
            mapper.Map(req, assignmentScore);
            db.Set<AssignmentScore>().Update(assignmentScore);
            try
            {
                var affected = await db.SaveChangesAsync(ct);
                if (affected > 0)
                {
                    return new UpdateAssignmentScoreResponse(true, "Updated successfully");
                }
                else
                {
                    return new UpdateAssignmentScoreResponse(false, "No rows affected");
                }
            }
            catch (DbUpdateException)
            {
                return new UpdateAssignmentScoreResponse(false, "Error while updating assignment score");
            }
        }
    }
}
