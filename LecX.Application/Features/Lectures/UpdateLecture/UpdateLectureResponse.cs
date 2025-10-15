using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;

namespace LecX.Application.Features.Lectures.UpdateLecture
{
    public sealed record UpdateLectureResponse(bool Success, string Message, LectureDTO? Data = null) : ResponseRecord<LectureDTO>(Success, Message, Data);
}