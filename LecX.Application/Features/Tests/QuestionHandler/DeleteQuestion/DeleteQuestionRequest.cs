using MediatR;

namespace LecX.Application.Features.Tests.QuestionHandler.DeleteQuestion
{
    public sealed record DeleteQuestionRequest(
        int QuestionId
        ) : IRequest<DeleteQuestionResponse>;
}