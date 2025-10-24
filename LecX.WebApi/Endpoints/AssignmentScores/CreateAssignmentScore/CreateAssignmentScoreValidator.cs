using System.ComponentModel.DataAnnotations;
using LecX.Application.Features.AssignmentScores.CreateAssignmentScore;
using FluentValidation;
using FastEndpoints;
namespace LecX.WebApi.Endpoints.AssignmentScores.CreateAssignmentScore
{
    public class CreateAssignmentScoreValidator : Validator<CreateAssignmentScoreRequest>
    {
        public CreateAssignmentScoreValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId is required.")
                .MaximumLength(50).WithMessage("StudentId cannot exceed 50 characters.");
            RuleFor(x => x.AssignmentId)
                .GreaterThan(0).WithMessage("AssignmentId must be a positive integer.");
            RuleFor(x => x.Score)
                .InclusiveBetween(0, 10).WithMessage("Score must be between 0 and 10.");
        }
    }
}
