using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.Comments.DeleteComment
{
    public sealed class DeleteCommentRequest : IRequest<DeleteCommentResponse>
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public int CommentId { get; set; }
    }
}