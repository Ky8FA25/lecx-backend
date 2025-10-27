using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.Lectures.GetLecturesCompletedByUser
{
    public sealed class GetLectureCompletedByUserRequest() : IRequest<GetLectureCompletedByUserResponse>
    {
        [JsonIgnore]
        public string UserId { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}