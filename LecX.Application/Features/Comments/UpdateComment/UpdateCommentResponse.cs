using LecX.Application.Common.Dtos;
using LecX.Application.Features.Comments.Common;

namespace LecX.Application.Features.Comments.UpdateComment
{
    public sealed record UpdateCommentResponse(
        string Message,
        bool Success = false,
        CommentDto? Data = null
    ) : GenericResponseRecord<CommentDto>(Success, Message, Data);
}