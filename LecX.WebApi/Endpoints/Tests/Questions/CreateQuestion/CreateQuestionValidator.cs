using FluentValidation;

namespace LecX.Application.Features.Tests.QuestionHandler.CreateQuestion
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestionRequest>
    {
        public CreateQuestionValidator()
        {
            // 🔹 TestId bắt buộc > 0
            RuleFor(x => x.TestId)
                .GreaterThan(0)
                .WithMessage("TestId is required and must be greater than 0.");

            // 🔹 Nội dung câu hỏi
            RuleFor(x => x.QuestionContent)
                .NotEmpty().WithMessage("Question content is required.")
                .MaximumLength(500).WithMessage("Question content cannot exceed 500 characters.");

            // 🔹 Các đáp án bắt buộc
            RuleFor(x => x.AnswerA)
                .NotEmpty().WithMessage("Answer A is required.")
                .MaximumLength(255);

            RuleFor(x => x.AnswerB)
                .NotEmpty().WithMessage("Answer B is required.")
                .MaximumLength(255);

            RuleFor(x => x.AnswerC)
                .NotEmpty().WithMessage("Answer C is required.")
                .MaximumLength(255);

            RuleFor(x => x.AnswerD)
                .NotEmpty().WithMessage("Answer D is required.")
                .MaximumLength(255);

            // 🔹 Đáp án đúng bắt buộc, chỉ cho phép "A", "B", "C" hoặc "D"
            RuleFor(x => x.CorrectAnswer)
                .NotEmpty().WithMessage("Correct answer is required.")
                .Must(answer => new[] { "A", "B", "C", "D" }.Contains(answer))
                .WithMessage("Correct answer must be one of: A, B, C, or D.");

            // 🔹 Ảnh (nếu có) — chỉ kiểm tra độ dài cơ bản
            RuleFor(x => x.ImagePath)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.ImagePath))
                .WithMessage("Image path cannot exceed 500 characters.");
        }
    }
}
