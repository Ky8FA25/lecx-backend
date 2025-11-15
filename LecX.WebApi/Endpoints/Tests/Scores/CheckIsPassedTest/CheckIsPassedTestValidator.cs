using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestScoreHandler.CheckIsPassedTest;

namespace LecX.WebApi.Endpoints.Tests.Scores.CheckIsPassedTest
{
    public sealed class CheckIsPassedTestValidator : Validator<CheckIsPassedTestRequest>
    {
        public CheckIsPassedTestValidator() 
        {
            RuleFor(x => x.TestId)
                .NotEmpty().WithMessage("TestId is required.");
        }
    }
}
