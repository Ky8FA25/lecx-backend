using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;

namespace LecX.Application.Features.Lectures.CreateLecture
{
    public sealed record CreateLectureResponse(bool Success, string Message, LectureDTO? Data): GenericResponseRecord<LectureDTO>(Success,Message,Data); 
}