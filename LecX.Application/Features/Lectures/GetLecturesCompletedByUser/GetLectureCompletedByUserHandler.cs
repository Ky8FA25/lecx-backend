using AutoMapper;
using LecX.Application.Abstractions;
using LecX.Application.Common.Pagination;
using LecX.Application.Features.Lectures.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Lectures.GetLecturesCompletedByUser
{
    public sealed class GetLectureCompletedByUserHandler(
        IAppDbContext db,
        IMapper mapper
    ) : IRequestHandler<GetLectureCompletedByUserRequest, GetLectureCompletedByUserResponse>
    {
        public async Task<GetLectureCompletedByUserResponse> Handle(GetLectureCompletedByUserRequest request, CancellationToken ct)
        {
            try
            {
                var lectureIds = await db.Set<LectureCompletion>()
                                            .AsNoTracking()
                                            .Include(lc => lc.Lecture)
                                            .Where(lc => lc.StudentId == request.UserId
                                                         && lc.Lecture.CourseId == request.CourseId)
                                            .OrderByDescending(lc => lc.CompletionDate)
                                            .Select(lc => lc.LectureId)
                                            .Distinct()
                                            .ToListAsync(ct);

                var query = db.Set<Lecture>()
                    .AsNoTracking()
                    .Where(l => lectureIds.Contains(l.LectureId))
                    .Include(l => l.LectureFiles)
                    .OrderByDescending(l => l.UpLoadDate);

                // Áp dụng phân trang
                var paginatedLectures = await PaginatedResponse<Lecture>.CreateAsync(
                    query,
                    request.PageIndex,
                    request.PageSize,
                    ct
                );

                var lectureDtos = paginatedLectures.MapItems(l => mapper.Map<LectureDTO>(l));

                // Trả về response
                return new GetLectureCompletedByUserResponse
                {
                    Success = true,
                    Message = "Retrieved completed lectures successfully.",
                    Data = lectureDtos
                };
            }
            catch (Exception ex)
            {
                return new GetLectureCompletedByUserResponse
                {
                    Success = false,
                    Message = $"Error retrieving completed lectures: {ex.Message}"
                };
            }
        }

    }
}
