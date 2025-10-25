using LecX.Domain.Enums;
using MediatR;

namespace LecX.Application.Features.Tests.TestHandler.UpdateTest
{
    public sealed record UpdateTestRequest(
        int TestId,
        string? Title,
        string? Description,
        int? CourseId,
        DateTime? StartTime,
        TimeSpan? TestTime,
        DateTime? EndTime,
        int? NumberOfQuestion,
        TestStatus? Status,
        double? PassingScore,
        string? AlowRedo,
        int? NumberOfMaxAttempt
        ) : IRequest<UpdateTestResponse>;
}