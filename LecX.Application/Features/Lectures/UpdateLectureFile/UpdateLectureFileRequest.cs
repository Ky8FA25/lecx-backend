using LecX.Domain.Enums;
using MediatR;

namespace LecX.Application.Features.Lectures.UpdateLectureFile
{
    public sealed record UpdateLectureFileRequest(
        int FileId,
        string? FileName,
        FileType? FileType,
        string? FilePath,
        string? FileExtension
        ) : IRequest<UpdateLectureFileResponse>;
}