using System.ComponentModel.DataAnnotations;
using FluentValidation;
using LecX.Application.Features.Submissions.DeleteSubmission;
using FastEndpoints;
namespace LecX.WebApi.Endpoints.Submissions.DeleteSubmission
{
    public class DeleteSubmissionValidator : Validator<DeleteSubmissionRequest>
    {
        public DeleteSubmissionValidator()
        {
            RuleFor(x => x.SubmissionId)
                .GreaterThan(0).WithMessage("SubmissionId must be greater than 0");
        }
    }
}
