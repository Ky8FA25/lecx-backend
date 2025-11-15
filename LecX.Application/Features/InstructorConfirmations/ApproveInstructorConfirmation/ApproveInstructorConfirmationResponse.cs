using LecX.Application.Common.Dtos;
using LecX.Application.Features.InstructorConfirmations.Common;

namespace LecX.Application.Features.InstructorConfirmations.ApproveInstructorConfirmation
{
    public sealed record ApproveInstructorConfirmationResponse(
        string Message,
        bool Success = false,
        InstructorConfirmationDto? Data = null
    ) : GenericResponseRecord<InstructorConfirmationDto>(Success, Message, Data);
}
