using LecX.Application.Common.Dtos;
using LecX.Application.Features.Comments.Common;

namespace LecX.Application.Features.Comments.CreateComment
{
    public sealed record CreateCommentResponse(
        string Message,
        bool Success = false,
        CommentDto? Data = null
    ) : GenericResponseRecord<CommentDto>(Success, Message, Data);
}