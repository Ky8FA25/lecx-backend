using System.ComponentModel.DataAnnotations;

namespace LecX.Domain.Entities
{
    public class Lecture
    {
        public int LectureId { get; set; }
        public int CourseId { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime UpLoadDate { get; set; } = DateTime.Now;
        public virtual Course Course { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
        public virtual IEnumerable<LectureCompletion> LectureCompletions { get; set; } = new List<LectureCompletion>();
        public virtual IEnumerable<LectureFile> LectureFiles { get; set; } = new List<LectureFile>();
    }
}