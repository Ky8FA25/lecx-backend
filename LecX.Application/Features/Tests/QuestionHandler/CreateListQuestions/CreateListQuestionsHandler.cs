using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.QuestionHandler.CreateListQuestions
{
    public sealed class CreateListQuestionsHandler(IAppDbContext db, IMapper mapper)
        : IRequestHandler<CreateListQuestionsRequest, CreateListQuestionsResponse>
    {
        public async Task<CreateListQuestionsResponse> Handle(CreateListQuestionsRequest request, CancellationToken ct)
        {
            try
            {
                // 🔹 Kiểm tra danh sách rỗng
                if (request.Questions == null || request.Questions.Count == 0)
                {
                    return new CreateListQuestionsResponse
                    {
                        Success = false,
                        Message = "List of questions in request is null or empty."
                    };
                }

                // 🔹 Lấy TestId đầu tiên từ danh sách (giả định tất cả cùng 1 TestId)
                var testId = request.Questions.First().TestId;

                // 🔹 Kiểm tra xem test có tồn tại không
                var test = await db.Set<Test>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TestId == testId, ct);

                if (test == null)
                {
                    return new CreateListQuestionsResponse
                    {
                        Success = false,
                        Message = "Test not found."
                    };
                }

                // 🔹 Đếm số lượng câu hỏi hiện tại
                int currentCount = await db.Set<Question>()
                    .CountAsync(q => q.TestId == testId, ct);

                // 🔹 Số lượng câu hỏi mới muốn thêm
                int newCount = request.Questions.Count;

                // 🔹 Kiểm tra vượt quá giới hạn
                if (currentCount + newCount > test.NumberOfQuestion)
                {
                    return new CreateListQuestionsResponse
                    {
                        Success = false,
                        Message = $"Cannot add {newCount} questions. " +
                                  $"Test already has {currentCount}/{test.NumberOfQuestion} questions."
                    };
                }

                // 🔹 Thêm câu hỏi mới
                var questionEntities = mapper.Map<List<Question>>(request.Questions);
                await db.Set<Question>().AddRangeAsync(questionEntities, ct);
                await db.SaveChangesAsync(ct);

                return new CreateListQuestionsResponse
                {
                    Success = true,
                    Message = "List of questions created successfully!"
                };
            }
            catch (Exception ex)
            {
                return new CreateListQuestionsResponse
                {
                    Success = false,
                    Message = $"An error occurred while creating list of questions: {ex.Message}"
                };
            }
        }
    }
}
