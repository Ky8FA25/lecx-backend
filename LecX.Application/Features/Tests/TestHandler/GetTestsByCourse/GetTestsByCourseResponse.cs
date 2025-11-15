using LecX.Application.Common.Dtos;
using LecX.Application.Features.Tests.Common;

namespace LecX.Application.Features.Tests.TestHandler.GetTestsByCourse
{
    public sealed class GetTestsByCourseResponse : GenericResponseClass<PaginatedResponse<TestDTO>>;
}