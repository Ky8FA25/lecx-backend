using MediatR;

namespace LecX.Application.Features.Lectures.CreateLectureCompletion
{
    public sealed record CreateLectureCompletionRequest(
        int LectureId,
        string StudentId
        ) : IRequest<CreateLectureCompletionResponse>;
}