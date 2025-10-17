using LecX.Application.Common.Dtos;
using LecX.Application.Common.Pagination;
using LecX.Application.Features.Lectures.Common;

namespace LecX.Application.Features.Lectures.GetLecturesByCourse
{
    public sealed class GetLectureByCourseResponse
        : GenericResponseClass<PaginatedResponse<LectureDTO>>;
}