using MediatR;

namespace LecX.Application.Features.Lectures.CreateLecture
{
    public sealed record CreateLectureRequest(
        int CourseId,
        string Title,
        string Description
        ) :IRequest<CreateLectureResponse>;
}