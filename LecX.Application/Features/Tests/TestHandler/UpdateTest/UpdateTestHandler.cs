using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Tests.TestHandler.UpdateTest
{
    public sealed class UpdateTestHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<UpdateTestRequest, UpdateTestResponse>
    {
        public async Task<UpdateTestResponse> Handle(UpdateTestRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var testEntity = await db.Set<Test>()
                    .FindAsync([ request.TestId ], cancellationToken);
                if (testEntity == null)
                {
                    return new UpdateTestResponse
                    {
                        Success = false,
                        Message = "Test not found."
                    };
                }
                if (!string.IsNullOrEmpty(request.Title))
                    testEntity.Title = request.Title;
                if (!string.IsNullOrEmpty(request.Description))
                    testEntity.Description = request.Description;
                if (request.CourseId.HasValue)
                    testEntity.CourseId = request.CourseId.Value;
                if (request.StartTime.HasValue)
                    testEntity.StartTime = request.StartTime.Value;
                if (request.TestTime.HasValue)
                    testEntity.TestTime = request.TestTime.Value;
                if (request.EndTime.HasValue)
                    testEntity.EndTime = request.EndTime.Value;
                if (request.NumberOfQuestion.HasValue)
                    testEntity.NumberOfQuestion = request.NumberOfQuestion.Value;
                if (request.Status.HasValue)
                    testEntity.Status = request.Status.Value;
                if (request.PassingScore.HasValue)
                    testEntity.PassingScore = request.PassingScore.Value;
                if (!string.IsNullOrEmpty(request.AlowRedo))
                    testEntity.AlowRedo = request.AlowRedo;
                if (request.NumberOfMaxAttempt.HasValue)
                    testEntity.NumberOfMaxAttempt = request.NumberOfMaxAttempt.Value;
                db.Set<Test>().Update(testEntity);
                await db.SaveChangesAsync(cancellationToken);
                return new UpdateTestResponse
                {
                    Success = true,
                    Message = "Test updated successfully.",
                    Data = mapper.Map<TestDTO>(testEntity)
                };

            }
            catch (Exception ex)
            {
                return new UpdateTestResponse
                {
                    Success = false,
                    Message = $"Error occurred while updating test: {ex.Message}"
                };
            }
        }
    }
}
