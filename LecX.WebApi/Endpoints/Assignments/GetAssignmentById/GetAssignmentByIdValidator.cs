using FluentValidation;
using FastEndpoints;
using LecX.Application.Features.Assignments.GetAssignmentById;
namespace LecX.WebApi.Endpoints.Assignments.GetAssignmentById
{
    public class GetAssignmentByIdValidator: Validator<GetAssignmentByIdRequest>
    {
        public GetAssignmentByIdValidator()
        {
            RuleFor(x => x.AssignmentId).GreaterThan(0).WithMessage("AssignmentId must greater than 0");
        }
    }
}
