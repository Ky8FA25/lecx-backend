using MediatR;

namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreByCourseId
{
    public sealed record GetAssignmentScoreByCourseIdRequest
    (
        int CourseId
    ) : IRequest<GetAssignmentScoreByCourseIdResponse>;
}