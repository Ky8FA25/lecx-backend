using AutoMapper;
using LecX.Application.Abstractions.InternalServices.Queues;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Utils;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.AssignmentScores.UpdateAssignmentScore
{
    public sealed class UpdateAssignmentScoreHandler(
        IAppDbContext db,
        IMapper mapper,
        IStudentCourseCompletionQueue queue
    ) : IRequestHandler<UpdateAssignmentScoreRequest, UpdateAssignmentScoreResponse>
    {
        public async Task<UpdateAssignmentScoreResponse> Handle(UpdateAssignmentScoreRequest req, CancellationToken ct)
        {
            var assignmentScore = await db.Set<AssignmentScore>()
                .SingleOrDefaultAsync(c => c.AssignmentScoreId == req.AssignmentScoreId, ct);
            if (assignmentScore is null)
                throw new KeyNotFoundException("Assignment score not found");

            mapper.Map(req, assignmentScore);
            db.Set<AssignmentScore>().Update(assignmentScore);

            try
            {
                var affected = await db.SaveChangesAsync(ct);

                var assignment = await db.Set<Assignment>()
                    .FirstOrDefaultAsync(a => a.AssignmentId == assignmentScore.AssignmentId, ct);

                if (assignment == null)
                {
                    return new UpdateAssignmentScoreResponse(false, "Assignment not found");
                }

                await queue.EnqueueAsync(assignmentScore.StudentId, assignment.CourseId);

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
