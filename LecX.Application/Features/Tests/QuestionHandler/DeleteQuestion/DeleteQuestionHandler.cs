using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.QuestionHandler.DeleteQuestion
{
    public sealed class DeleteQuestionHandler(IAppDbContext dbContext) : IRequestHandler<DeleteQuestionRequest, DeleteQuestionResponse>
    {
        public async Task<DeleteQuestionResponse> Handle(DeleteQuestionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var question = await dbContext.Set<Question>()
                .FirstOrDefaultAsync(q => q.QuestionId == request.QuestionId, cancellationToken);
                if (question is null)
                {
                    return new DeleteQuestionResponse
                    {
                        Success = false,
                        Message = "Question not found."
                    };
                }
                dbContext.Set<Question>().Remove(question);
                await dbContext.SaveChangesAsync(cancellationToken);
                return new DeleteQuestionResponse
                {
                    Success = true,
                    Message = "Question deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new DeleteQuestionResponse
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
