using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Lectures.CreateLectureCompletion
{
    public class CreateLectureCompletionHandler(IAppDbContext db, IMapper mapper)
        : IRequestHandler<CreateLectureCompletionRequest, CreateLectureCompletionResponse>
    {
        public async Task<CreateLectureCompletionResponse> Handle(CreateLectureCompletionRequest request, CancellationToken ct)
        {
            try
            {
                //Lấy Lecture để biết CourseId
                var lecture = await db.Set<Lecture>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.LectureId == request.LectureId, ct);

                if (lecture == null)
                {
                    return new CreateLectureCompletionResponse
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
                    return new CreateLectureCompletionResponse
                    {
                        Success = false,
                        Message = "Student is not enrolled in this course."
                    };
                }

                //Check unique (đã có cặp studentId & lectureId chưa)
                var existedLectureCompletion = await db.Set<LectureCompletion>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.LectureId == request.LectureId && l.StudentId == request.StudentId, ct);

                if (existedLectureCompletion != null)
                {
                    return new CreateLectureCompletionResponse
                    {
                        Success = false,
                        Message = "Lecture is already completed and recorded."
                    };
                }

                //Tạo bản ghi completion mới
                var lectureCompletion = mapper.Map<LectureCompletion>(request);
                lectureCompletion.CompletionDate = DateTime.Now;
                await db.Set<LectureCompletion>().AddAsync(lectureCompletion, ct);
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

                return new CreateLectureCompletionResponse
                {
                    Success = true,
                    Message = "Lecture completion recorded successfully."
                };
            }
            catch (Exception ex)
            {
                return new CreateLectureCompletionResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
