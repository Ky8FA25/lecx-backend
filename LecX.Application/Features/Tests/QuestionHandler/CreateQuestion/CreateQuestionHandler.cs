using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.QuestionHandler.CreateQuestion
{
    public sealed class CreateQuestionHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<CreateQuestionRequest, CreateQuestionResponse>
    {
        public async Task<CreateQuestionResponse> Handle(CreateQuestionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // 🔹 Lấy test hiện tại
                var test = await db.Set<Test>()
                    .Include(t => t.Questions)
                    .FirstOrDefaultAsync(t => t.TestId == request.TestId, cancellationToken);

                if (test == null)
                {
                    return new CreateQuestionResponse
                    {
                        Success = false,
                        Message = "Test not found."
                    };
                }

                // 🔹 Kiểm tra xem số câu hỏi đã đủ chưa
                int currentQuestionCount = test.Questions.Count();
                if (currentQuestionCount >= test.NumberOfQuestion)
                {
                    return new CreateQuestionResponse
                    {
                        Success = false,
                        Message = $"Cannot add more questions. The test already has {currentQuestionCount}/{test.NumberOfQuestion} questions."
                    };
                }

                var questionEntity = mapper.Map<Question>(request);
                await db.Set<Question>().AddAsync(questionEntity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                var questionDto = mapper.Map<QuestionDTO>(questionEntity);
                return new CreateQuestionResponse
                {
                    Success = true,
                    Data = questionDto,
                    Message = "Question created successfully."
                };
            }
            catch (Exception ex)
            {
                return new CreateQuestionResponse
                {
                    Success = false,
                    Message = $"An error occurred while creating the question: {ex.Message}"
                };
            }
        }
    }
}
