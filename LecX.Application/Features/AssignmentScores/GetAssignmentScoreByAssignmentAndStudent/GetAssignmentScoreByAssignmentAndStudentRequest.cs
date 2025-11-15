using LecX.Application.Features.AssignmentScores.GetAssignmentScoreByAssignmentAndStudent;
using MediatR;


namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreByAssignmentAndStudent
{
    public sealed record GetAssignmentScoreByAssignmentAndStudentRequest
    ( int AssignmentId, string StudentId
    ): IRequest<GetAssignmentScoreByAssignmentAndStudentResponse>;
}
