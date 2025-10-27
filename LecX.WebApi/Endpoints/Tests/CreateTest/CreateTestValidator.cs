using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestHandler.CreateTest;

namespace LecX.WebApi.Endpoints.Tests.CreateTest
{
    public class CreateTestValidator : Validator<CreateTestRequest>
    {
        public CreateTestValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
            RuleFor(x => x.StartTime)
                .GreaterThan(DateTime.Now).WithMessage("Start time must be in the future.");
            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
            RuleFor(x => x.NumberOfQuestion)
                .GreaterThan(0).WithMessage("Number of questions must be greater than zero.");
            RuleFor(x => x.PassingScore)
                .InclusiveBetween(0, 10).WithMessage("Passing score must be between 0 and 10.")
                .When(x => x.PassingScore.HasValue);
            RuleFor(x => x.AlowRedo)
                .NotEmpty().WithMessage("AllowRedo is required.")
                .Must(value => value == "Yes" || value == "No")
                .WithMessage("AlowRedo must be either 'Yes' or 'No'.");
            RuleFor(x => x.NumberOfMaxAttempt)
                .GreaterThan(0).WithMessage("Number of max attempts must be greater than zero.")
                .When(x => x.AlowRedo == "Yes");
        }
    }
}
