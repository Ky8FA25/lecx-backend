using AutoMapper;
using AutoMapper.QueryableExtensions;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.TestScoreHandler.GetTestScoreById
{
    public sealed class GetTestScoreByIdHandler(IAppDbContext db, IMapper mapper)
        : IRequestHandler<GetTestScoreByIdRequest, GetTestScoreByIdResponse>
    {
        public async Task<GetTestScoreByIdResponse> Handle(GetTestScoreByIdRequest request, CancellationToken ct)
        {
            try
            {
                var testScore = await db.Set<TestScore>().AsNoTracking()
                                                .Where(x => x.TestScoreId == request.TestScoreId)
                                                .Include(x => x.Test)
                                                .Include(x => x.Student)
                                                .ProjectTo<TestScoreDTO>(mapper.ConfigurationProvider)
                                                .SingleOrDefaultAsync();
                if (testScore == null)
                {
                    return new GetTestScoreByIdResponse
                    {
                        Success = false,
                        Message = "TestScoreId not found"
                    };
                }
                return new GetTestScoreByIdResponse
                {
                    Success = true,
                    Message = "Test score retrived successfully.",
                    Data = testScore
                };
            }
            catch (Exception ex)
            {
                return new GetTestScoreByIdResponse
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
