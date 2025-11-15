using LecX.Domain.Enums;
using MediatR;

namespace LecX.Application.Features.Tests.TestHandler.GetTestsByCourse
{
    public sealed record GetTestsByCourseRequest(
        int CourseId,
        string? Keyword,
        DateTime? Date,
        TestStatus? TestStatus,
        int PageNumber = 1,
        int PageSize = 10) : IRequest<GetTestsByCourseResponse>;
}