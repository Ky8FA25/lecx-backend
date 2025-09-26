using System.ComponentModel.DataAnnotations;

namespace LecX.Domain.Entities
{
    public class BookMark
    {
        public int BookmarkId { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; } 

        // Navigation properties
        public virtual User Student { get; set; } 
        public virtual Course Course { get; set; }

    }
}