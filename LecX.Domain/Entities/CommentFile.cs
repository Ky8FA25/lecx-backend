namespace LecX.Domain.Entities
{
    public class CommentFile
    {
        public int FileId { get; set; }
        public int CommentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public virtual Comment Comment { get; set; }
    }
}