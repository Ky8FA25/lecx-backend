using MediatR;

namespace LecX.Application.Features.Lectures.DeleteLectureCompletion
{
    public sealed record DeleteLectureCompletionRequest(int LectureId,
        string StudentId) : IRequest<DeleteLectureCompletionResponse>;
}