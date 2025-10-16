
using LecX.Application.Features.Assignments.AssignmentsDtos;
using LecX.Application.Common.Dtos;
namespace LecX.Application.Features.Assignments.GetAssignmentById
{
    public sealed record GetAssignmentByIdResponse
    (
        bool Success,
        string Message,
        AssignmentDto Data = null

    ): GenericResponseRecord<AssignmentDto>(Success, Message, Data);
}
