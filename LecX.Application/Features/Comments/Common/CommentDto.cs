namespace LecX.Application.Features.Comments.Common
{
    public sealed class CommentDto
    {
        public int CommentId { get; set; }
        public int LectureId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ParentCmtId { get; set; }
        public CommentUserDto User { get; set; }
        public CommentFileDto? File { get; set; }
    }

    public sealed class CommentUserDto
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public sealed class CommentFileDto
    {
        public string FilePath { get; set; }
    }
}
