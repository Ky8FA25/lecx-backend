using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Lectures.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Lectures.GetStudentCompletedLecture
{
    public sealed class GetStudentCompletedLectureHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetStudentCompletedLectureRequest, GetStudentCompletedLectureResponse>
    {
        public async Task<GetStudentCompletedLectureResponse> Handle(GetStudentCompletedLectureRequest request, CancellationToken ct)
        {
            try
            {
                var query = db.Set<LectureCompletion>()
                    .Where(sl => sl.LectureId == request.LectureId)
                    .Include(sl => sl.Student)
                    .AsQueryable();
                // 🔹 Lọc theo Keyword
                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    var kw = request.Keyword.Trim().ToLower();
                    query = query.Where(c =>
                        c.Student.FirstName.ToLower().Contains(kw) ||
                        (c.Student.LastName != null && c.Student.LastName.ToLower().Contains(kw))
                    );
                }
                query = query.OrderByDescending(c => c.Student.FirstName);
                // 🔹 Phân trang + map DTO
                var paginated = await PaginatedResponse<LectureCompletion>.CreateAsync(query, request.PageIndex, request.PageSize, ct);
                var result = paginated.MapItems(c => mapper.Map<LectureCompletionDTO>(c));
                return new GetStudentCompletedLectureResponse
                {
                    Success = true,
                    Data = result,
                    Message = "List students completed this lecture retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new GetStudentCompletedLectureResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving students completed lectures: {ex.Message}"
                };
            }
        }
    }
}
