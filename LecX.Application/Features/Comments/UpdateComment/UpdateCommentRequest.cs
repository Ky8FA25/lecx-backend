using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.Comments.UpdateComment
{
    public sealed class UpdateCommentRequest : IRequest<UpdateCommentResponse>
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
