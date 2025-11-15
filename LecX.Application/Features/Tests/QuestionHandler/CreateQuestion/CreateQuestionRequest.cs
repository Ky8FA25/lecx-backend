using MediatR;

namespace LecX.Application.Features.Tests.QuestionHandler.CreateQuestion
{
    public sealed record CreateQuestionRequest(
        int TestId,
        string QuestionContent,
        string AnswerA,
        string AnswerB,
        string AnswerC,
        string AnswerD,
        string CorrectAnswer,
        string? ImagePath = null
        ) : IRequest<CreateQuestionResponse>;
}