using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;

namespace LecX.Application.Features.Lectures.GetStudentCompletedLecture
{
    public sealed class GetStudentCompletedLectureResponse : GenericResponseClass<PaginatedResponse<LectureCompletionDTO>>;
}