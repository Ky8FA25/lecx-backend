using System.ComponentModel.DataAnnotations;
using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Submissions.CreateSubmission;
namespace LecX.WebApi.Endpoints.Submissions.CreateSubmission
{
    public class CreateSubmissionValidator : Validator<CreateSubmissionRequest>
    {
        public CreateSubmissionValidator()
        {
            RuleFor(x => x.AssignmentId).GreaterThan(0);
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId is required.");

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
