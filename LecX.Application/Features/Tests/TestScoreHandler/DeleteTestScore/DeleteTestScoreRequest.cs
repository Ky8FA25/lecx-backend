using MediatR;

namespace LecX.Application.Features.Tests.TestScoreHandler.DeleteTestScore
{
    public sealed record DeleteTestScoreRequest(
        int TestScoreId
        ) : IRequest<DeleteTestScoreResponse>;
}