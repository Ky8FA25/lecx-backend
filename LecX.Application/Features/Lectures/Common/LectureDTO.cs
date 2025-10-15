
namespace LecX.Application.Features.Lectures.Common
{
    public sealed class LectureDTO
    {
        public int? LectureId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpLoadDate { get; set; } = DateTime.Now;
        public List<LectureFileDto> LectureFiles { get; set; } = new List<LectureFileDto>();
    }
}
