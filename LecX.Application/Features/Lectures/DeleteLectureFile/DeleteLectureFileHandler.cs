using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Lectures.DeleteLectureFile
{
    public class DeleteLectureFileHandler(IAppDbContext dbContext) : IRequestHandler<DeleteLectureFileRequest, DeleteLectureFileResponse>
    {
        public async Task<DeleteLectureFileResponse> Handle(DeleteLectureFileRequest request, CancellationToken ct)
        {
            try
            {
                var lectureFile = await dbContext.Set<LectureFile>().FindAsync([request.FileId], ct);
                if(lectureFile == null)
                {
                    return new DeleteLectureFileResponse(false, "Lecture file not found.");
                }
                dbContext.Set<LectureFile>().Remove(lectureFile);
                await dbContext.SaveChangesAsync(ct);
                return new DeleteLectureFileResponse(true, "Lecture file deleted successfully.");
            }
            catch (Exception ex)
            {
                return new DeleteLectureFileResponse(false, $"Error deleting lecture file: {ex.Message}");
            }
        }
    }
}
