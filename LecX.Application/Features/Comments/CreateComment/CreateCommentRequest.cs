using MediatR;
namespace LecX.Application.Features.Comments.CreateComment
{
    public sealed class CreateCommentRequest : IRequest<CreateCommentResponse>
    {
        public int LectureId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public int? ParentCmtId { get; set; }
    }
}
