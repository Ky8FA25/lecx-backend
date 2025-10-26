using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.QuestionHandler.DeleteQuestion;

namespace LecX.WebApi.Endpoints.Tests.Questions.DeleteQuestion
{
    public class DeleteQuestionValidator : Validator<DeleteQuestionRequest>
    {
        public DeleteQuestionValidator() 
        {
            RuleFor(x => x.QuestionId).NotEmpty().WithMessage("QuestionId is required.");
        }

    }
}
