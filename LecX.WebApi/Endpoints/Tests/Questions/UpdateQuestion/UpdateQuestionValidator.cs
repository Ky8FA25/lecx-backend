using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.QuestionHandler.UpdateQuestion;

namespace LecX.WebApi.Endpoints.Tests.Questions.UpdateQuestion
{
    public sealed class UpdateQuestionValidator : Validator<UpdateQuestionRequest>
    {
        public UpdateQuestionValidator()
        {
            // 🔹 QuestionId bắt buộc
            RuleFor(x => x.QuestionId)
                .GreaterThan(0)
                .WithMessage("QuestionId must be greater than 0.");

            // 🔹 Chỉ validate nếu không null
            RuleFor(x => x.QuestionContent)
                .MaximumLength(500)
                .When(x => x.QuestionContent != null)
                .WithMessage("QuestionContent cannot exceed 500 characters.");

            RuleFor(x => x.AnswerA)
                .MaximumLength(200)
                .When(x => x.AnswerA != null)
                .WithMessage("AnswerA cannot exceed 200 characters.");

            RuleFor(x => x.AnswerB)
                .MaximumLength(200)
                .When(x => x.AnswerB != null)
                .WithMessage("AnswerB cannot exceed 200 characters.");

            RuleFor(x => x.AnswerC)
                .MaximumLength(200)
                .When(x => x.AnswerC != null)
                .WithMessage("AnswerC cannot exceed 200 characters.");

            RuleFor(x => x.AnswerD)
                .MaximumLength(200)
                .When(x => x.AnswerD != null)
                .WithMessage("AnswerD cannot exceed 200 characters.");

            // 🔹 Chỉ cho phép CorrectAnswer có giá trị A, B, C hoặc D
            RuleFor(x => x.CorrectAnswer)
                .Must(a => new[] { "A", "B", "C", "D" }.Contains(a))
                .When(x => x.CorrectAnswer != null)
                .WithMessage("CorrectAnswer must be one of A, B, C, or D.");

            // 🔹 ImagePath: kiểm tra độ dài nếu có
            RuleFor(x => x.ImagePath)
                .MaximumLength(500)
                .When(x => x.ImagePath != null)
                .WithMessage("ImagePath cannot exceed 500 characters.");
        }
    }
}
