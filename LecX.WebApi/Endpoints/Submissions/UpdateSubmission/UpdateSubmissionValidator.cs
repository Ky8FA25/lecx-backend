using FluentValidation;
using System.Data;
using FastEndpoints;
using LecX.Application.Features.Submissions.UpdateSubmission;
namespace LecX.WebApi.Endpoints.Submissions.UpdateSubmission
{
    public class UpdateSubmissionValidator : Validator<UpdateSubmissionRequest>
    {
        public UpdateSubmissionValidator() 
        {
            RuleFor(x => x.SubmissionLink)
                .NotEmpty().WithMessage("SubmissionLink is required.")
                .MaximumLength(2048).WithMessage("SubmissionLink must not exceed 2048 characters.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("SubmissionLink must be a valid URL.");
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(255).WithMessage("FileName must not exceed 255 characters.");

        }
    }
}
