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
        public ICollection<CommentFileDto>? CommentFileDtos { get; set; }
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
        public int FileId { get; set; }
        public int CommentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
