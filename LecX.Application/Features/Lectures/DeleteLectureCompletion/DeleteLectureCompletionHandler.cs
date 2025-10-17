using LecX.Application.Abstractions;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Lectures.DeleteLectureCompletion
{
    public class DeleteLectureCompletionHandler(IAppDbContext db) : IRequestHandler<DeleteLectureCompletionRequest, DeleteLectureCompletionResponse>
    {
        public async Task<DeleteLectureCompletionResponse> Handle(DeleteLectureCompletionRequest request, CancellationToken ct)
        {
            try
            {
                //Lấy Lecture để biết CourseId
                var lecture = await db.Set<Lecture>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.LectureId == request.LectureId, ct);

                if (lecture == null)
                {
                    return new DeleteLectureCompletionResponse
                    {
                        Success = false,
                        Message = "Lecture not found."
                    };
                }

                //Kiểm tra xem student có tham gia course này không
                var studentCourse = await db.Set<StudentCourse>()
                    .FirstOrDefaultAsync(sc => sc.StudentId == request.StudentId && sc.CourseId == lecture.CourseId, ct);

                if (studentCourse == null)
                {
                    return new DeleteLectureCompletionResponse
                    {
                        Success = false,
                        Message = "Student is not enrolled in this course."
                    };
                }

                //Check existed record
                var existedLectureCompletion = await db.Set<LectureCompletion>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.LectureId == request.LectureId && l.StudentId == request.StudentId, ct);

                if (existedLectureCompletion == null)
                {
                    return new DeleteLectureCompletionResponse
                    {
                        Success = false,
                        Message = "Lecture has not been completed and recorded yet."
                    };
                }
                db.Set<LectureCompletion>().Remove(existedLectureCompletion);
                await db.SaveChangesAsync(ct);

                //Tính toán progress mới của học viên
                var totalLectures = await db.Set<Lecture>()
                    .CountAsync(l => l.CourseId == lecture.CourseId, ct);

                var completedLectures = await db.Set<LectureCompletion>()
                    .CountAsync(lc => lc.StudentId == request.StudentId && lc.Lecture.CourseId == lecture.CourseId, ct);

                // Tính phần trăm hoàn thành
                decimal progress = totalLectures > 0
                    ? Math.Round((decimal)completedLectures / totalLectures * 100, 2)
                    : 0;

                studentCourse.Progress = progress;
                await db.SaveChangesAsync(ct);

                return new DeleteLectureCompletionResponse
                {
                    Success = true,
                    Message = "Lecture completion record deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new DeleteLectureCompletionResponse
                {
                    Success = false,
                    Message = $"An error occurred while deleting the lecture completion record: {ex.Message}"
                };
            }
        }
    }
}
