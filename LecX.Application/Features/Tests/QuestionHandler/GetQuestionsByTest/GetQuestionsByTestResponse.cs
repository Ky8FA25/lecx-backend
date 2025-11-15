using LecX.Application.Common.Dtos;
using LecX.Application.Features.Tests.Common;

namespace LecX.Application.Features.Tests.QuestionHandler.GetQuestionsByTest
{
    public sealed class GetQuestionsByTestResponse : GenericResponseClass<PaginatedResponse<QuestionDTO>>;
}