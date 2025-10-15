using MediatR;

namespace LecX.Application.Features.Lectures.UpdateLecture
{
    public sealed record UpdateLectureRequest(
        int LectureId,
        string? Title,
        string? Description
        ) : IRequest<UpdateLectureResponse>;
}