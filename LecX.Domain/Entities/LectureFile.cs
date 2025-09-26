using LecX.Domain.Enums;

namespace LecX.Domain.Entities
{
    public class LectureFile
    {
        public int FileId { get; set; }
        public int LectureId { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; } 
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;

        // Navigation property
        public virtual Lecture Lecture { get; set; }
    }
}