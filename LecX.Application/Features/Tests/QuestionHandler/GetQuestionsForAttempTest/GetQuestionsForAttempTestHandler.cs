using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.QuestionHandler.GetQuestionsForAttempTest
{
    public sealed class GetQuestionsForAttempTestHandler(IAppDbContext db, IMapper mapper)
        : IRequestHandler<GetQuestionsForAttempTestRequest, GetQuestionsForAttempTestResponse>
    {
        public async Task<GetQuestionsForAttempTestResponse> Handle(GetQuestionsForAttempTestRequest request, CancellationToken ct)
        {
            try
            {
                var questions = await db.Set<Question>()
                                        .AsNoTracking()
                                        .Where(q => q.TestId == request.TestId)
                                        .ToListAsync(ct);
                var dtos = questions.Select(q =>
                {
                    var dto = mapper.Map<QuestionDTO>(q);
                    dto.CorrectAnswer = null;
                    return dto;
                }).ToList();

                return new GetQuestionsForAttempTestResponse
                {
                    Success = true,
                    Data = dtos,
                    Message = "Questions retrieved successfully for attempt test."
                };
            }
            catch (Exception ex)
            {
                return new GetQuestionsForAttempTestResponse
                {
                    Success = false,
                    Message = $"An error occurred while retrieving questions for attempt test: {ex.Message}"
                };
            }
        }
    }
}
