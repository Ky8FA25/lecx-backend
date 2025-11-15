using MediatR;

namespace LecX.Application.Features.Tests.TestScoreHandler.GetTestScoreById
{
    public sealed record GetTestScoreByIdRequest(
        int TestScoreId
        ) : IRequest<GetTestScoreByIdResponse>;
}