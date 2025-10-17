using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Lectures.UpdateLectureFile
{
    public class UpdateLectureFileHandler(IAppDbContext db)
        : IRequestHandler<UpdateLectureFileRequest, UpdateLectureFileResponse>
    {
        public async Task<UpdateLectureFileResponse> Handle(UpdateLectureFileRequest request, CancellationToken ct)
        {
            try
            {
                // Tìm file theo ID
                var lectureFile = await db.Set<LectureFile>().FindAsync([request.FileId], ct);
                if (lectureFile == null)
                {
                    return new UpdateLectureFileResponse(false, "FileId not found", null);
                }

                // Cập nhật các trường nếu có giá trị mới
                if (!string.IsNullOrWhiteSpace(request.FileName))
                    lectureFile.FileName = request.FileName;

                if (request.FileType.HasValue)
                    lectureFile.FileType = request.FileType.Value;

                if (!string.IsNullOrWhiteSpace(request.FilePath))
                    lectureFile.FilePath = request.FilePath;

                if (!string.IsNullOrWhiteSpace(request.FileExtension))
                    lectureFile.FileExtension = request.FileExtension;

                // Cập nhật ngày sửa đổi (nếu có cột)
                lectureFile.UploadDate = DateTime.UtcNow;

                // Lưu thay đổi
                await db.SaveChangesAsync(ct);

                return new UpdateLectureFileResponse(
                    true,
                    "Lecture file updated successfully.",
                    null
                );
            }
            catch (Exception ex)
            {
                return new UpdateLectureFileResponse(
                    false,
                    $"Error: {ex.Message}",
                    null
                );
            }
        }
    }
}
