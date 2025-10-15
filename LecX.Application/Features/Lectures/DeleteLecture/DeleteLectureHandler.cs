using LecX.Application.Abstractions;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Lectures.DeleteLecture
{
    public class DeleteLectureHandler(IAppDbContext db) : IRequestHandler<DeleteLectureRequest, DeleteLectureResponse>
    {
        public async Task<DeleteLectureResponse> Handle(DeleteLectureRequest request, CancellationToken ct)
        {
            try
            {
                var lecture = await db.Set<Lecture>().FindAsync([request.LectureId], ct);
                if (lecture == null)
                {
                    return new DeleteLectureResponse(false, "LectureId not found");
                }
                db.Set<Lecture>().Remove(lecture);
                return new DeleteLectureResponse(true, "Delete lecture successfully!");
            }
            catch (Exception ex)
            {
                return new DeleteLectureResponse(false, $"Error deleting lecture: {ex.Message}");
            }
                        
        }
    }
}
