using LecX.Application.Common.Dtos;
using LecX.Application.Features.Comments.Common;

namespace LecX.Application.Features.Comments.CreateComment
{
    public sealed class CreateCommentResponse : AbstractResponse
    {
        public CommentDto? Comment { get; set; }
    }
}