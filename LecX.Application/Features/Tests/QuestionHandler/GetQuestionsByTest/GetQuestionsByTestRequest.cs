using MediatR;

namespace LecX.Application.Features.Tests.QuestionHandler.GetQuestionsByTest
{
    public sealed record GetQuestionsByTestRequest(
        int TestId,
        int PageIndex,
        int PageSize
        ) : IRequest<GetQuestionsByTestResponse>;
}