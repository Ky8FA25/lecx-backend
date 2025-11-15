using MediatR;

namespace LecX.Application.Features.InstructorConfirmations.ApproveInstructorConfirmation
{
    public sealed class ApproveInstructorConfirmationRequest : IRequest<ApproveInstructorConfirmationResponse>
    {
        public int ConfirmationId { get; set; }
    }
}
