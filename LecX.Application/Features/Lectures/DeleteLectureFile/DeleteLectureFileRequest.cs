using MediatR;

namespace LecX.Application.Features.Lectures.DeleteLectureFile
{
    public sealed record DeleteLectureFileRequest(int FileId) : IRequest<DeleteLectureFileResponse>;
}