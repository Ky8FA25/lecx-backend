using LecX.Application.Common.Dtos;
using LecX.Application.Features.Tests.Common;

namespace LecX.Application.Features.Tests.TestScoreHandler.GetStudentScoresByTest
{
    public sealed class GetStudentScoresByTestResponse : GenericResponseClass<PaginatedResponse<TestScoreDTO>>;
}