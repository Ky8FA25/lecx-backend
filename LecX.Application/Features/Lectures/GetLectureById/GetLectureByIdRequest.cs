using MediatR;

namespace LecX.Application.Features.Lectures.GetLectureById
{
    public sealed record GetLectureByIdRequest(
        int LectureId
    ) : IRequest<GetLectureByIdResponse>;
}