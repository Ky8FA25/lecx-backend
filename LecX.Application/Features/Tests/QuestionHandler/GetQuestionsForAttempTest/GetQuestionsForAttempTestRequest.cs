using MediatR;

namespace LecX.Application.Features.Tests.QuestionHandler.GetQuestionsForAttempTest
{
    public sealed record GetQuestionsForAttempTestRequest(
        int TestId
        ) : IRequest<GetQuestionsForAttempTestResponse>;
}