using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestScoreHandler.GetTestScoreById;

namespace LecX.WebApi.Endpoints.Tests.Scores.GetTestScoreById
{
    public sealed class GetTestScoreByIdValidator : Validator<GetTestScoreByIdRequest>
    {
        public GetTestScoreByIdValidator() 
        {
            RuleFor(x => x.TestScoreId)
                .NotEmpty().WithMessage("TestScoreId is required.");
        }
    }
}
