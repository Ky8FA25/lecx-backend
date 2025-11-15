using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Tests.TestHandler.CreateTest
{
    public sealed class CreateTestHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<CreateTestRequest, CreateTestResponse>
    {
        public async Task<CreateTestResponse> Handle(CreateTestRequest request, CancellationToken ct)
        {
            try
            {
                var testEntity = mapper.Map<Test>(request);
                await db.Set<Test>().AddAsync(testEntity, ct);
                await db.SaveChangesAsync(ct);
                return new CreateTestResponse
                {
                    Success = true,
                    Message = "Test created successfully.",
                    Data = mapper.Map<TestDTO>(testEntity)
                };
            }
            catch (Exception ex)
            {
                return new CreateTestResponse
                {
                    Success = false,
                    Message = $"Error occurred while creating test: {ex.Message}"
                };
            }
        }
    }
}
