using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Tests.TestHandler.GetTestById
{
    public sealed class GetTestByIdHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetTestByIdRequest, GetTestByIdResponse>
    {
        public async Task<GetTestByIdResponse> Handle(GetTestByIdRequest request, CancellationToken ct)
        {
            try
            {
                var testEntity = await db.Set<Test>()
                    .FindAsync([ request.TestId ], ct);
                if (testEntity == null)
                {
                    return new GetTestByIdResponse
                    {
                        Success = false,
                        Message = "Test not found."
                    };
                }
                return new GetTestByIdResponse
                {
                    Success = true,
                    Message = "Test retrieved successfully.",
                    Data = mapper.Map<TestDTO>(testEntity)
                };
            }
            catch (Exception ex)
            {
                return new GetTestByIdResponse
                {
                    Success = false,
                    Message = $"Error occurred while retrieving test: {ex.Message}"
                };
            }
        }
    }
}
