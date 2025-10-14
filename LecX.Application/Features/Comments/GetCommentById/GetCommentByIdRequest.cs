using MediatR;

namespace LecX.Application.Features.Comments.GetCommentById
{
    public sealed class GetCommentByIdRequest : IRequest<GetCommentByIdResponse>
    {
        public int CommentId { get; set; }
    }
}