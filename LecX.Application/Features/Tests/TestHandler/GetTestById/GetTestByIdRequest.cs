using MediatR;

namespace LecX.Application.Features.Tests.TestHandler.GetTestById
{
    public sealed record GetTestByIdRequest(
        int TestId
        ) : IRequest<GetTestByIdResponse>;
}