using LecX.Application.Common.Dtos;
using LecX.Application.Features.Comments.Common;

namespace LecX.Application.Features.Comments.GetCommentById
{
    public sealed class GetCommentByIdResponse : AbstractResponse
    {
        public CommentDto Comment { get; set; } = null!;
    }
}