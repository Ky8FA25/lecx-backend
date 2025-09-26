using LecX.Domain.Enums;

namespace LecX.Domain.Entities
{
    public class CourseMaterial
    {
        public int MaterialID { get; set; }
        public int CourseID { get; set; }
        public FileType FileType { get; set; }
        public string FIleName { get; set; }
        public string FileExtension { get; set; }
        public string MaterialsLink { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;

        public virtual Course Course { get; set; }
    }
}