using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.AssignmentScores.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreByAssignmentAndStudent
{
    public sealed class GetAssignmentScoreByAssignmentAndStudentHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetAssignmentScoreByAssignmentAndStudentRequest, GetAssignmentScoreByAssignmentAndStudentResponse>
    {
        public async Task<GetAssignmentScoreByAssignmentAndStudentResponse> Handle(GetAssignmentScoreByAssignmentAndStudentRequest req, CancellationToken ct)
        {
            var assignmentScore = await db.Set<AssignmentScore>()
                .FirstOrDefaultAsync(a => a.AssignmentId == req.AssignmentId && a.StudentId == req.StudentId, ct);
            if (assignmentScore is null)
            {
                return new GetAssignmentScoreByAssignmentAndStudentResponse(false, "No found", null);
            }
            var assignmentScoreDto = mapper.Map<AssignmentScoreDto>(assignmentScore);
            return new GetAssignmentScoreByAssignmentAndStudentResponse(true, "Assignment Score retrieved successfully", assignmentScoreDto);
        }
    }
}
