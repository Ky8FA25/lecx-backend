using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;

namespace LecX.Application.Features.Lectures.CreateLectureFile
{
    public sealed record CreateLectureFileResponse(bool Success, string Message, LectureFileDto? Data) 
        : GenericResponseRecord<LectureFileDto>(Success,Message,Data);
}