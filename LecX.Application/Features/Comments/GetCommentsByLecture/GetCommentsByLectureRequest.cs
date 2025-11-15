using MediatR;

namespace LecX.Application.Features.Comments.GetCommentsByLecture
{
    public class GetCommentsByLectureRequest : IRequest<GetCommentsByLectureResponse>
    {
        public int LectureId { get; set; }
        public int? ParentCmtId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}