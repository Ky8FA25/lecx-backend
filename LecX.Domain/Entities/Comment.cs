namespace LecX.Domain.Entities
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int LectureID { get; set; }
        public string UserID { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int? ParentCmtId { get; set; }
        public virtual Lecture Lecture { get; set; }
        public virtual User User { get; set; }
        public virtual Comment ParentComment { get; set; }

        public virtual IEnumerable<CommentFile> CommentFiles { get; set; } = new List<CommentFile>();
    }
}