using MediatR;

namespace LecX.Application.Features.Lectures.GetLecturesByCourse
{
    public sealed record GetLectureByCourseRequest(
        int CourseId,
        int PageIndex = 1,
        int PageSize = 10
    ) : IRequest<GetLectureByCourseResponse>;
}
