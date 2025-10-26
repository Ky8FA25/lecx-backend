using MediatR;

namespace LecX.Application.Features.Tests.QuestionHandler.UpdateQuestion
{
    public sealed record UpdateQuestionRequest(
        int QuestionId,
        string? QuestionContent,
        string? AnswerA,
        string? AnswerB,
        string? AnswerC,
        string? AnswerD,
        string? CorrectAnswer,
        string? ImagePath
        ) : IRequest<UpdateQuestionResponse>;
}