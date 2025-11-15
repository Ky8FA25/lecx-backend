using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.InstructorConfirmations.ApproveInstructorConfirmation;

namespace LecX.WebApi.Endpoints.InstructorConfirmations.ApproveInstructorConfirmation
{
    public class ApproveInstructorConfirmationValidator : Validator<ApproveInstructorConfirmationRequest>
    {
        public ApproveInstructorConfirmationValidator()
        {
            RuleFor(x => x.ConfirmationId)
                .GreaterThan(0).WithMessage("Confirmation ID must be greater than 0.");
        }
    }
}
