using LecX.Domain.Enums;

namespace LecX.Application.Features.Lectures.Common
{
    public sealed class LectureFileDto
    {
        public int? FileId { get; set; }
        public int LectureId { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
