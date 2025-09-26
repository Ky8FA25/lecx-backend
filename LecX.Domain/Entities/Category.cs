using LecX.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LecX.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public CourseCategory FullName { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; }
    }
}