using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestHandler.UpdateTest;

namespace LecX.WebApi.Endpoints.Tests.UpdateTest
{
    public sealed class UpdateTestValidator : Validator<UpdateTestRequest>
    {
        public UpdateTestValidator()
        {
            RuleFor(x => x.TestId)
                .NotEmpty().WithMessage("TestId is required.");
            RuleFor(x => x.Title)
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.")
                .When(x => x.Title != null);

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .When(x => x.Description != null);

            RuleFor(x => x.StartTime)
                .GreaterThan(DateTime.Now).WithMessage("StartTime must be in the future.")
                .When(x => x.StartTime.HasValue);

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime!.Value).WithMessage("End time must be after start time.")
                .When(x => x.EndTime.HasValue && x.StartTime.HasValue);

            RuleFor(x => x.NumberOfQuestion)
                .GreaterThan(0).WithMessage("Number of questions must be greater than zero.")
                .When(x => x.NumberOfQuestion.HasValue);

            RuleFor(x => x.PassingScore)
                .InclusiveBetween(0, 10).WithMessage("Passing score must be between 0 and 10.")
                .When(x => x.PassingScore.HasValue);

            RuleFor(x => x.AlowRedo)
                .Must(value => value == "Yes" || value == "No")
                .When(x => x.AlowRedo != null)
                .WithMessage("AlowRedo must be either 'Yes' or 'No'.");

            RuleFor(x => x.NumberOfMaxAttempt)
                .GreaterThan(0).WithMessage("Number of max attempts must be greater than zero.")
                .When(x => x.AlowRedo == "Yes" && x.NumberOfMaxAttempt.HasValue);
        }
    }
}
