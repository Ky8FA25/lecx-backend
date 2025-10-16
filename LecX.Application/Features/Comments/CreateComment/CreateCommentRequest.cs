using MediatR;
using System.Text.Json.Serialization;
namespace LecX.Application.Features.Comments.CreateComment
{
    public sealed class CreateCommentRequest : IRequest<CreateCommentResponse>
    {
        public int LectureId { get; set; }
        public string Content { get; set; }
        public int? ParentCmtId { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
