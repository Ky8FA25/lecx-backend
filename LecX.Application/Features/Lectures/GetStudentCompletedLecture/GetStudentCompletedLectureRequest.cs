using MediatR;

namespace LecX.Application.Features.Lectures.GetStudentCompletedLecture
{
    public sealed record GetStudentCompletedLectureRequest(
        int LectureId,
        string? Keyword,
        int PageIndex,
        int PageSize
        ) : IRequest<GetStudentCompletedLectureResponse>;
}