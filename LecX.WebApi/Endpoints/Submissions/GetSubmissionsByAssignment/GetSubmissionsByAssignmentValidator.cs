using System.ComponentModel.DataAnnotations;
using FluentValidation;
using LecX.Application.Features.Submissions.GetSubmissionsByAssignment;
using FastEndpoints;
namespace LecX.WebApi.Endpoints.Submissions.GetSubmissionsByAssignment
{
    public class GetSubmissionsByAssignmentValidator : Validator<GetSubmissionsByAssignmentRequest>
    {
        public GetSubmissionsByAssignmentValidator()
        {
            RuleFor(x => x.AssignmentId)
                .GreaterThan(0).WithMessage("AssignmentId must be greater than 0.");
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0).WithMessage("PageIndex must be greater than or equal to 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("PageSize must be less than or equal to 100.");
        }
    }
}
