using MediatR;

namespace LecX.Application.Features.Comments.DeleteComment
{
    public sealed class DeleteCommentRequest : IRequest<DeleteCommentResponse>
    {
        public int CommentId { get; set; }
    }
}