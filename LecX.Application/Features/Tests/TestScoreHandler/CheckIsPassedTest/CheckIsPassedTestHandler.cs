using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.TestScoreHandler.CheckIsPassedTest
{
    public sealed class CheckIsPassedTestHandler(IAppDbContext db) : IRequestHandler<CheckIsPassedTestRequest, CheckIsPassedTestResponse>
    {
        public async Task<CheckIsPassedTestResponse> Handle(CheckIsPassedTestRequest request, CancellationToken ct)
        {
            try
            {
                var test = await db.Set<Test>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TestId == request.TestId, ct);
                if (test == null)
                {
                    return new CheckIsPassedTestResponse
                    {
                        Success = false,
                        Message = "Cannot find the test."
                    };
                }
                var highestTestScore = await db.Set<TestScore>()
                              .AsNoTracking()
                              .Where(ts => ts.StudentId == request.StudentId && ts.TestId == request.TestId)
                              .OrderByDescending(ts => ts.ScoreValue)
                              .FirstOrDefaultAsync();
                if (highestTestScore == null || highestTestScore.ScoreValue < test.PassingScore)
                {
                    return new CheckIsPassedTestResponse
                    {
                        Success = true,
                        Data = false,
                        Message = "Test not passed."
                    };
                } else
                {
                    return new CheckIsPassedTestResponse
                    {
                        Success = true,
                        Data = true,
                        Message = "Test passed."
                    };
                }

            }
            catch (Exception ex)
            {
                return new CheckIsPassedTestResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
