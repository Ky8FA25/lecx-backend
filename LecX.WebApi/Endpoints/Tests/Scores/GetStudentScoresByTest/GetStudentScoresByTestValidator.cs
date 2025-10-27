using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestScoreHandler.GetStudentScoresByTest;

namespace LecX.WebApi.Endpoints.Tests.Scores.GetStudentScoresByTest
{
    public class GetStudentScoresByTestValidator : Validator<GetStudentScoresByTestRequest>
    {
        public GetStudentScoresByTestValidator()
        {
            RuleFor(x => x.TestId)
                .NotEmpty().WithMessage("TestId is required.");
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageIndex must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
