using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestScoreHandler.CreateTestScore;

namespace LecX.WebApi.Endpoints.Tests.Scores.CreateTestScore
{
    public class CreateTestScoreValidator : Validator<CreateTestScoreRequest>
    {
        public CreateTestScoreValidator() 
        {
            RuleFor(x => x.TestId)
                .NotEmpty().WithMessage("TestId is required.");
            // 🔹 Danh sách trả lời không được null hoặc rỗng
            RuleFor(x => x.Answers)
                .NotNull().WithMessage("Answers list cannot be null.")
                .Must(list => list.Count > 0)
                .WithMessage("Answers list must contain at least one answer.");
        }
    }
}
