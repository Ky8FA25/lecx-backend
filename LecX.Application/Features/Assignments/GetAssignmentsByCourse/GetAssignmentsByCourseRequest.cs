using MediatR;

namespace LecX.Application.Features.Assignments.GetAssignmentsByCourse
{
    public sealed record GetAssignmentsByCourseRequest
    (
        string? SearchWord,
        int? CourseId,
        DateTime? DateSearch,
        int PageIndex,
        int PageSize
    ) :IRequest<GetAssignmentsByCourseResponse>;
}
