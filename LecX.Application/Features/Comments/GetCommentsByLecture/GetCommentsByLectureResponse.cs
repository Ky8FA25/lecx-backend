using LecX.Application.Common.Dtos;
using LecX.Application.Common.Pagination;
using LecX.Application.Features.Comments.Common;

namespace LecX.Application.Features.Comments.GetCommentsByLecture
{
    public class GetCommentsByLectureResponse : AbstractResponse
    {
        public PaginatedList<CommentDto>? Data { get; set; }
    }
}