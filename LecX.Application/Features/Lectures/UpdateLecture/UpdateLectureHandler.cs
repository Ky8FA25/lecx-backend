using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Lectures.UpdateLecture
{
    public sealed class UpdateLectureHandler(IAppDbContext db) : IRequestHandler<UpdateLectureRequest, UpdateLectureResponse>
    {
        public async Task<UpdateLectureResponse> Handle(UpdateLectureRequest request, CancellationToken ct)
        {
            try
            {
                var lecture = await db.Set<Lecture>().FindAsync(request.LectureId);
                if (lecture == null) return new UpdateLectureResponse(false, "LectureId not found");
                if(!string.IsNullOrEmpty(request.Title)) lecture.Title = request.Title;
                if (!string.IsNullOrEmpty(request.Description)) lecture.Description = request.Description;
                lecture.UpLoadDate = DateTime.Now;
                await db.SaveChangesAsync(ct);
                return new UpdateLectureResponse(true, "Update lecture successfully!");
            }
            catch (Exception ex)
            {
                return new UpdateLectureResponse(false, $"Error deleting lecture: {ex.Message}");
            }

        }
    }
}
