using System.ComponentModel.DataAnnotations;

namespace LecX.Domain.Entities
{
    public class BookMark
    {
        public int BookmarkID { get; set; }
        public string StudentID { get; set; }
        public int CourseID { get; set; } 

        // Navigation properties
        public virtual User Student { get; set; } 
        public virtual Course Course { get; set; }

    }
}