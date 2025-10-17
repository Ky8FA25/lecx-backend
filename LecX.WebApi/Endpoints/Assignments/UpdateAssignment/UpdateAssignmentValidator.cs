using FluentValidation;
using FastEndpoints;
using LecX.Application.Features.Assignments.UpdateAssignment;
namespace LecX.WebApi.Endpoints.Assignments.UpdateAssignment
{
    public class UpdateAssignmentValidator : Validator<UpdateAssignmentRequest>
    {
        public UpdateAssignmentValidator() 
        { RuleFor(x => x.Title).NotEmpty().MaximumLength(200).WithMessage("Title is required and not exceed 200 words ");
          RuleFor(x => x.StartDate).LessThan(x => x.DueDate).WithMessage("Start date must be earlier than due date.");
          RuleFor(x => x.DueDate).GreaterThan(x => x.StartDate).WithMessage("Due date must be later than start date.");
          RuleFor(x => x.AssignmentLink).NotEmpty().MaximumLength(500);
        }
    }
}
