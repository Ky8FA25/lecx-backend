using MediatR;

namespace LecX.Application.Features.Tests.TestHandler.DeleteTest
{
    public sealed record DeleteTestRequest(
        int TestId
        ) : IRequest<DeleteTestResponse>;
}