using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.TestHandler.GetTestsByCourse
{
    public sealed class GetTestsByCourseHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetTestsByCourseRequest, GetTestsByCourseResponse>
    {
        public async Task<GetTestsByCourseResponse> Handle(GetTestsByCourseRequest request, CancellationToken ct)
        {
            try
            {
                var query = db.Set<Test>().AsNoTracking()
                    .Where(t => t.CourseId == request.CourseId)
                    .AsQueryable();
                // 🔹 Lọc theo Keyword
                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    var kw = request.Keyword.Trim().ToLower();
                    query = query.Where(c =>
                        c.Title.ToLower().Contains(kw) ||
                        (c.Description != null && c.Description.ToLower().Contains(kw))
                    );
                }
                // 🔹 Lọc theo TestStatus
                if (request.TestStatus.HasValue)
                    query = query.Where(c => c.Status == request.TestStatus);
                // 🔹 Lọc theo Date
                if (request.Date.HasValue)
                {
                    var date = request.Date.Value.Date;
                    var nextDate = date.AddDays(1);

                    query = query.Where(c => c.StartTime >= date && c.StartTime < nextDate);
                }
                query = query.OrderByDescending(c => c.StartTime);
                // 🔹 Phân trang + map DTO
                var paginated = await PaginatedResponse<Test>.CreateAsync(query, request.PageNumber, request.PageSize, ct);
                var result = paginated.MapItems(c => mapper.Map<TestDTO>(c));
                return new GetTestsByCourseResponse
                {
                    Success = true,
                    Data = result,
                    Message = "Tests retrieved successfully."
                };

            }
            catch (Exception ex)
            {
                return new GetTestsByCourseResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving tests: {ex.Message}"
                };
            }
        }
    }
}
