using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestScoreHandler.DeleteTestScore;

namespace LecX.WebApi.Endpoints.Tests.Scores.DeleteTestScore
{
    public sealed class DeleteTestScoreValidator : Validator<DeleteTestScoreRequest>
    {
        public DeleteTestScoreValidator() 
        {
            RuleFor(x => x.TestScoreId)
                .NotEmpty().WithMessage("TestScoreId is required.");
        }
    }
}
