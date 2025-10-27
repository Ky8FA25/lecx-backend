using MediatR;

namespace LecX.Application.Features.Tests.TestScoreHandler.GetStudentScoresByTest
{
    public sealed record GetStudentScoresByTestRequest(
        int TestId,
        string? Keyword,
        bool? OrderByScore,
        int PageIndex,
        int PageSize
        ) : IRequest<GetStudentScoresByTestResponse>;
}