using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.QuestionHandler.GetQuestionById;

namespace LecX.WebApi.Endpoints.Tests.Questions.GetQuestionById
{
    public sealed class GetQuestionByIdValidator : Validator<GetQuestionByIdRequest>
    {
        public GetQuestionByIdValidator()
        {
            RuleFor(x => x.QuestionId).NotEmpty().WithMessage("QuestionId is required.");
        }
    }
}
