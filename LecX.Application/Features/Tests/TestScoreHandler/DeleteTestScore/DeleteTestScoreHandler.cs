using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Tests.TestScoreHandler.DeleteTestScore
{
    public sealed class DeleteTestScoreHandler(IAppDbContext db) : IRequestHandler<DeleteTestScoreRequest, DeleteTestScoreResponse>
    {
        public async Task<DeleteTestScoreResponse> Handle(
            DeleteTestScoreRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var testScore = await db.Set<TestScore>()
                    .FindAsync([request.TestScoreId], cancellationToken);
                if (testScore is null)
                {
                    return new DeleteTestScoreResponse
                    {
                        Success = false,
                        Message = "Test score not found."
                    };
                }
                db.Set<TestScore>().Remove(testScore);
                await db.SaveChangesAsync(cancellationToken);
                return new DeleteTestScoreResponse
                {
                    Success = true,
                    Message = "Test score deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new DeleteTestScoreResponse
                {
                    Success = false,
                    Message = $"An error occurred while deleting the test score: {ex.Message}"
                };
            }
        }
    }
}
