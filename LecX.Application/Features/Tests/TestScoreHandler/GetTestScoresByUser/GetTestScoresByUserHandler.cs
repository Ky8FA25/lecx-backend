using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.TestScoreHandler.GetTestScoresByUser
{
    public sealed class GetTestScoresByUserHandler(IAppDbContext db, IMapper mapper)
        : IRequestHandler<GetTestScoresByUserRequest, GetTestScoresByUserResponse>
    {
        public async Task<GetTestScoresByUserResponse> Handle(GetTestScoresByUserRequest request, CancellationToken ct)
        {
            try
            {
                var query = db.Set<TestScore>()
                              .AsNoTracking()
                              .Where(ts => ts.StudentId == request.StudentId)
                              .Include(ts => ts.Test)
                              .AsQueryable();

                // 🔹 Nếu có TestId, lọc theo TestId
                if (request.TestId.HasValue)
                {
                    query = query.Where(ts => ts.TestId == request.TestId.Value);
                }
                // 🔹 Nếu không có TestId mà có CourseId, lọc theo CourseId của Test
                else if (request.CourseId.HasValue)
                {
                    query = query.Where(ts => ts.Test.CourseId == request.CourseId.Value);
                }
                query.OrderByDescending(x => x.DoTestAt);
                // 🔹 Phân trang + map DTO
                var paginated = await PaginatedResponse<TestScore>.CreateAsync(query, request.PageIndex, request.PageSize, ct);
                var result = paginated.MapItems(c => mapper.Map<TestScoreDTO>(c));
                return new GetTestScoresByUserResponse
                {
                    Success = true,
                    Data = result,
                    Message = "List scores of test retrived successfully."
                };
            }
            catch (Exception ex)
            {
                return new GetTestScoresByUserResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
