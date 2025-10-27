using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.TestScoreHandler.GetStudentScoresByTest
{
    public sealed class GetStudentScoresByTestHandler(IAppDbContext db, IMapper mapper)
        : IRequestHandler<GetStudentScoresByTestRequest, GetStudentScoresByTestResponse>
    {
        public async Task<GetStudentScoresByTestResponse> Handle(GetStudentScoresByTestRequest request, CancellationToken ct)
        {
            try
            {
                var query = db.Set<TestScore>()
                                    .AsNoTracking()
                                    .Where(x => x.TestId == request.TestId)
                                    .Include(x => x.Student)
                                    .AsQueryable();
                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    var kw = request.Keyword.Trim().ToLower();
                    query = query.Where(c =>
                        c.Student.FirstName.ToLower().Contains(kw) ||
                        (c.Student.LastName != null && c.Student.LastName.ToLower().Contains(kw))
                    );
                }
                if (request.OrderByScore == true)
                {
                    query = query.OrderBy(x => x.ScoreValue);
                }
                else if (request.OrderByScore == false)
                {
                    query = query.OrderByDescending(x => x.ScoreValue);
                }
                else query = query.OrderBy(x => x.Student.FirstName);
                // 🔹 Phân trang + map DTO
                var paginated = await PaginatedResponse<TestScore>.CreateAsync(query, request.PageIndex, request.PageSize, ct);
                var result = paginated.MapItems(c => mapper.Map<TestScoreDTO>(c));
                return new GetStudentScoresByTestResponse
                {
                    Success = true,
                    Data = result,
                    Message = "List scores of test retrived successfully."
                }; 
            }
            catch (Exception ex)
            {
                return new GetStudentScoresByTestResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
