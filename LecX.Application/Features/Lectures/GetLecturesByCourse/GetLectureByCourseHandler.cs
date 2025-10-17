using AutoMapper;
using LecX.Application.Abstractions;
using LecX.Application.Common.Pagination;
using LecX.Application.Features.Lectures.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Lectures.GetLecturesByCourse
{
    public sealed class GetLectureByCourseHandler(
        IAppDbContext db,
        IMapper mapper
    ) : IRequestHandler<GetLectureByCourseRequest, GetLectureByCourseResponse>
    {
        public async Task<GetLectureByCourseResponse> Handle(GetLectureByCourseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = db.Set<Lecture>()
                    .AsNoTracking()
                    .Where(l => l.CourseId == request.CourseId)
                    .Include(l => l.LectureFiles)
                    .OrderByDescending(l => l.UpLoadDate);

                var paginatedLectures = await PaginatedResponse<Lecture>.CreateAsync(
                    query,
                    request.PageIndex,
                    request.PageSize,
                    cancellationToken
                );

                // Map sang DTO
                var lectureDtos = paginatedLectures.MapItems(l => mapper.Map<LectureDTO>(l));

                return new GetLectureByCourseResponse
                {
                    Success = true,
                    Message = "Lectures retrieved successfully.",
                    Data = lectureDtos
                };
            }
            catch (Exception ex)
            {
                return new GetLectureByCourseResponse
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}
