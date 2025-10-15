using LecX.Application.Common.Dtos;
using LecX.Application.Features.Comments.Common;

namespace LecX.Application.Features.Comments.DeleteComment
{
    public sealed record DeleteCommentResponse(
        bool Success = false,
        string Message = ""
        ) : GenericResponseRecord<CommentDto>(Success, Message);
}