using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.Tests.TestScoreHandler.GetTestScoresByUser
{
    public sealed class GetTestScoresByUserRequest(
        ) : IRequest<GetTestScoresByUserResponse>
    {
        [JsonIgnore]
        public string StudentId { get; set; } = string.Empty;
        public int? CourseId { get; set; }
        public int? TestId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}