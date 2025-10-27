using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.TestScoreHandler.CreateTestScore
{
    public sealed class CreateTestScoreHandler(IAppDbContext db, IMapper mapper) :
        IRequestHandler<CreateTestScoreRequest, CreateTestScoreResponse>
    {
        public async Task<CreateTestScoreResponse> Handle(CreateTestScoreRequest request, CancellationToken ct)
        {
            try
            {
                // 🔹 1. Lấy thông tin bài test
                var test = await db.Set<Test>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TestId == request.TestId, ct);

                if (test == null)
                {
                    return new CreateTestScoreResponse
                    {
                        Success = false,
                        Message = "Cannot find the test."
                    };
                }

                // 🔹 2. Lấy số lần thi trước của học sinh
                var previousAttempts = await db.Set<TestScore>()
                    .CountAsync(ts => ts.StudentId == request.StudentId.ToString() && ts.TestId == request.TestId, ct);

                // 🔹 3. Kiểm tra điều kiện AlowRedo và NumberOfMaxAttempt
                bool allowRedo = test.AlowRedo.Equals("Yes", StringComparison.OrdinalIgnoreCase);
                bool hasMaxAttempt = test.NumberOfMaxAttempt.HasValue;

                if (!allowRedo && previousAttempts > 0)
                {
                    return new CreateTestScoreResponse
                    {
                        Success = false,
                        Message = "This test doesn't allow to redo."
                    };
                }

                if (hasMaxAttempt && previousAttempts >= test.NumberOfMaxAttempt)
                {
                    return new CreateTestScoreResponse
                    {
                        Success = false,
                        Message = $"You have reached the number of max attemp in test({test.NumberOfMaxAttempt})."
                    };
                }

                // 🔹 4. Lấy danh sách câu hỏi
                var questions = await db.Set<Question>()
                    .AsNoTracking()
                    .Where(q => q.TestId == request.TestId)
                    .ToListAsync(ct);

                if (!questions.Any())
                {
                    return new CreateTestScoreResponse
                    {
                        Success = false,
                        Message = "Can not find any question in the test."
                    };
                }

                // 🔹 5. Chấm điểm
                int correctCount = 0;
                foreach (var answer in request.Answers)
                {
                    var question = questions.FirstOrDefault(q => q.QuestionId == answer.QuestionId);
                    if (question != null &&
                        string.Equals(question.CorrectAnswer.Trim(), answer.SelectedAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        correctCount++;
                    }
                }

                double scoreValue = Math.Round((double)correctCount / questions.Count * 10, 2);
                int nextAttempt = previousAttempts + 1;

                // 🔹 6. Lưu điểm
                var testScore = new TestScore
                {
                    StudentId = request.StudentId.ToString(),
                    TestId = request.TestId,
                    DoTestAt = DateTime.UtcNow,
                    ScoreValue = scoreValue,
                    NumberOfAttempt = nextAttempt,
                };

                await db.Set<TestScore>().AddAsync(testScore, ct);
                await db.SaveChangesAsync(ct);
                testScore.Test = test;
                var result = mapper.Map<TestScoreDTO>(testScore);
                return new CreateTestScoreResponse
                {
                    Success = true,
                    Data = result,
                    Message = "Create test score successfully."
                };
            }
            catch (Exception ex)
            {
                return new CreateTestScoreResponse
                {
                    Success = false,
                    Message = $"An error occurred while creating the test score: {ex.Message}"
                };
            }
        }
    }
}
