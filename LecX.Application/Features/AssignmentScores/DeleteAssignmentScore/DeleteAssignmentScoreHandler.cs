using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.AssignmentScores.Common;
using LecX.Application.Features.AssignmentScores.CreateAssignmentScore;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LecX.Application.Features.AssignmentScores.DeleteAssignmentScore
{
    public sealed class DeleteAssignmentScoreHandler(IAppDbContext db) : IRequestHandler<DeleteAssignmentScoreRequest, DeleteAssignmentScoreResponse>
    {
        public async Task<DeleteAssignmentScoreResponse> Handle(DeleteAssignmentScoreRequest req, CancellationToken ct)
        {
            var assignmentScore = await db.Set<AssignmentScore>()
               .FindAsync(new object?[] { req.AssignmentScoreId }, ct);
            if (assignmentScore is null)
                throw new KeyNotFoundException("Assignment score not found");
            db.Set<AssignmentScore>().Remove(assignmentScore);

            try
            {
                var affected = await db.SaveChangesAsync(ct);
                if (affected > 0)
                {
                    return new DeleteAssignmentScoreResponse(true, "Deleted successfully");
                }
                else
                {
                    return new DeleteAssignmentScoreResponse(false, "No rows affected");
                }
            }

            catch (DbUpdateException)
            {
                return new DeleteAssignmentScoreResponse(false, "Error while creating assignment score");
            }
        }
    }
}
