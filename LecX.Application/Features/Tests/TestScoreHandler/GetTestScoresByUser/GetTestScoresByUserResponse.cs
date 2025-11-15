using LecX.Application.Common.Dtos;
using LecX.Application.Features.Tests.Common;

namespace LecX.Application.Features.Tests.TestScoreHandler.GetTestScoresByUser
{
    public sealed class GetTestScoresByUserResponse : GenericResponseClass<PaginatedResponse<TestScoreDTO>>;
}