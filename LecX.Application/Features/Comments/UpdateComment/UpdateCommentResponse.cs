using LecX.Application.Common.Dtos;
using LecX.Application.Features.Comments.Common;

namespace LecX.Application.Features.Comments.UpdateComment
{
    public sealed class UpdateCommentResponse : AbstractResponse
    {
        public CommentDto? Comment { get; set; }
    }
}