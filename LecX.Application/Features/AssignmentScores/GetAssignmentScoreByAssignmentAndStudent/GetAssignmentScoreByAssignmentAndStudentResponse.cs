using LecX.Application.Common.Dtos;
using LecX.Application.Features.AssignmentScores.Common;


namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreByAssignmentAndStudent
{
    public sealed record GetAssignmentScoreByAssignmentAndStudentResponse(bool Success, string Message, AssignmentScoreDto? Data) : GenericResponseRecord<AssignmentScoreDto>(Success, Message, Data);

}
