using MediatR;

namespace LecX.Application.Features.Comments.UpdateComment
{
    public sealed class UpdateCommentRequest : IRequest<UpdateCommentResponse>
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
