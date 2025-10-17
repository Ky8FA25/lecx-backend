using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;

namespace LecX.Application.Features.Lectures.GetLecturesCompletedByUser
{
    public sealed class GetLectureCompletedByUserResponse : GenericResponseClass<PaginatedResponse<LectureDTO>>;
}