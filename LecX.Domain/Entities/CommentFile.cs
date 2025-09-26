namespace LecX.Domain.Entities
{
    public class CommentFile
    {
        public int FileID { get; set; }
        public int CommentID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public virtual Comment Comment { get; set; }
    }
}