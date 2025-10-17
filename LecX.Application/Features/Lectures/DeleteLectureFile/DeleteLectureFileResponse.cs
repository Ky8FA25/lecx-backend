using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;
namespace LecX.Application.Features.Lectures.DeleteLectureFile
{
    public sealed record DeleteLectureFileResponse(bool Success, string Message)
        : GenericResponseRecord<LectureFileDto>(Success, Message);
}