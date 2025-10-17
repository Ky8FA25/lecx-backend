using LecX.Application.Common.Dtos;
using LecX.Application.Features.Comments.Common;

namespace LecX.Application.Features.Comments.GetCommentsByLecture
{
    public class GetCommentsByLectureResponse : GenericResponseClass<PaginatedList<CommentDto>?>;
}