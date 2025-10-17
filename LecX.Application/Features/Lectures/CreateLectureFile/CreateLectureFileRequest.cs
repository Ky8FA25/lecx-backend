using LecX.Domain.Enums;
using MediatR;

namespace LecX.Application.Features.Lectures.CreateLectureFile
{
    public sealed record CreateLectureFileRequest(
        int LectureId,
        string FileName,
        FileType FileType,
        string FilePath,
        string FileExtension
        ) : IRequest<CreateLectureFileResponse>;
}