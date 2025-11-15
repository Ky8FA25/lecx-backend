using LecX.Application.Features.Tests.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.Tests.TestScoreHandler.CreateTestScore
{
    public sealed class CreateTestScoreRequest : IRequest<CreateTestScoreResponse>
    {
        public required int TestId { get; set; }
        [JsonIgnore]
        public string StudentId { get; set; } = string.Empty;
        public required List<AnswerDTO> Answers { get; set; }
    }
}