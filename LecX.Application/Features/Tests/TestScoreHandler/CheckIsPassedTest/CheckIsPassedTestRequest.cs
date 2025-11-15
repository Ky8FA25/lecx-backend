using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.Tests.TestScoreHandler.CheckIsPassedTest
{
    public sealed class CheckIsPassedTestRequest : IRequest<CheckIsPassedTestResponse>
    {
        [JsonIgnore]
        public string StudentId { get; set; } = string.Empty;
        public int TestId { get; set; }
    }
}