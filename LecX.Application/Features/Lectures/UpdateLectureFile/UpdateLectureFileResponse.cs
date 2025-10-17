using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;

namespace LecX.Application.Features.Lectures.UpdateLectureFile
{
    public sealed record UpdateLectureFileResponse(bool Success, string Message, LectureFileDto? Data)
        : GenericResponseRecord<LectureFileDto>(Success, Message, Data);
}