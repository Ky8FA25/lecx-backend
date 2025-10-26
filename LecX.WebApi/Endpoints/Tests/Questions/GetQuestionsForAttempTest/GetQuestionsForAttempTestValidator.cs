using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.QuestionHandler.GetQuestionsForAttempTest;

namespace LecX.WebApi.Endpoints.Tests.Questions.GetQuestionsForAttempTest
{
    public class GetQuestionsForAttempTestValidator : Validator<GetQuestionsForAttempTestRequest>
    {
        public GetQuestionsForAttempTestValidator()
        {
            RuleFor(x => x.TestId).NotEmpty().WithMessage("TestId is required.");
        }
    }
}
