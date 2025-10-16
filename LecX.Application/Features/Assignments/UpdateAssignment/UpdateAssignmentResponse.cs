using LecX.Application.Common.Dtos;
using LecX.Application.Features.Assignments.AssignmentsDtos;
namespace LecX.Application.Features.Assignments.UpdateAssignment
{
    public sealed record UpdateAssignmentResponse
    (
        bool Success,
        string Message,
        AssignmentDto? Data = null
    ): GenericResponseRecord<AssignmentDto>(Success, Message, Data);
}
