using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Assignments.DeleteAssignment;


namespace LecX.WebApi.Endpoints.Assignments.DeleteAssignment
{
    public sealed class DeleteAssignmentValidator: Validator<DeleteAssignmentRequest>
    {
        public DeleteAssignmentValidator()
        {
            RuleFor(x => x.AssignmentId).GreaterThan(0).WithMessage("AssignmentId must be greater than 0.");
        }
    }
}
