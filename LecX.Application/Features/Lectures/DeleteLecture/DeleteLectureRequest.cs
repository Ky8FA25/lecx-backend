using MediatR;

namespace LecX.Application.Features.Lectures.DeleteLecture
{
    public sealed record DeleteLectureRequest(int LectureId) : IRequest<DeleteLectureResponse>;
}