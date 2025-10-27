using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.QuestionHandler.CreateListQuestions;

namespace LecX.WebApi.Endpoints.Tests.Questions.CreateListQuestions
{
    public class CreateListQuestionsValidator : Validator<CreateListQuestionsRequest>
    {
        public CreateListQuestionsValidator() 
        {
            // 🔹 Danh sách câu hỏi không được null hoặc rỗng
            RuleFor(x => x.Questions)
                .NotNull().WithMessage("Question list cannot be null.")
                .Must(list => list.Count > 0)
                .WithMessage("Question list must contain at least one question.");
        }
    }
}
