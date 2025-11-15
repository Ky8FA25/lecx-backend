using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Tests.TestHandler.DeleteTest
{
    public sealed class DeleteTestHandler(IAppDbContext db) : IRequestHandler<DeleteTestRequest, DeleteTestResponse>
    {
        public async Task<DeleteTestResponse> Handle(DeleteTestRequest request, CancellationToken ct)
        {
            try
            {
                var testEntity = await db.Set<Test>()
                    .FindAsync([ request.TestId ], ct);
                if (testEntity == null)
                {
                    return new DeleteTestResponse
                    {
                        Success = false,
                        Message = "Test not found."
                    };
                }
                db.Set<Test>().Remove(testEntity);
                await db.SaveChangesAsync(ct);
                return new DeleteTestResponse
                {
                    Success = true,
                    Message = "Test deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new DeleteTestResponse
                {
                    Success = false,
                    Message = $"Error occurred while deleting test: {ex.Message}"
                };
            }
        }
    }
}
