using LecX.Application.Features.Tests.Common;
using MediatR;

namespace LecX.Application.Features.Tests.QuestionHandler.CreateListQuestions
{
    public sealed class CreateListQuestionsRequest : IRequest<CreateListQuestionsResponse>
    {
        public required List<QuestionDTO> Questions { get; set; }
    }
}