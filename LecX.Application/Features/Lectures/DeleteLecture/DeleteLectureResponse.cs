using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;

namespace LecX.Application.Features.Lectures.DeleteLecture
{
    public sealed record DeleteLectureResponse(bool Success, string Message, LectureDTO? Data = null) : GenericResponseRecord<LectureDTO>(Success, Message, Data);
}