using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.QuestionHandler.GetQuestionsByTest
{
    public sealed class GetQuestionsByTestHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetQuestionsByTestRequest, GetQuestionsByTestResponse>
    {
        public async Task<GetQuestionsByTestResponse> Handle(GetQuestionsByTestRequest request, CancellationToken ct)
        {
            try
            {
                var query = db.Set<Question>()
                    .AsNoTracking()
                    .Where(q => q.TestId == request.TestId)
                    .AsQueryable();
                var paginated = await PaginatedResponse<Question>.CreateAsync(query, request.PageIndex, request.PageSize, ct);
                var result = paginated.MapItems(c => mapper.Map<QuestionDTO>(c));
                return new GetQuestionsByTestResponse
                {
                    Success = true,
                    Data = result,
                    Message = "Questions retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new GetQuestionsByTestResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving questions: {ex.Message}"
                };
            }
        }
    }
}
