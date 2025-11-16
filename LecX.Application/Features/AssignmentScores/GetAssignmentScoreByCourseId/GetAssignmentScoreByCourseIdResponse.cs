using LecX.Application.Common.Dtos;
using LecX.Application.Features.AssignmentScores.Common;

namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreByCourseId
{
    public sealed record GetAssignmentScoreByCourseIdResponse(bool Success,
    string Message,
    List<AssignmentScoreFullDataDto>? Data);
 
}