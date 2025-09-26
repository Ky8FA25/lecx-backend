using System.ComponentModel.DataAnnotations;
using LecX.Domain.Enums;

namespace LecX.Domain.Entities
{
    public class Course
    {
        public int CourseID { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(20)]
        public string CourseCode { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        public string CoverImagePath { get; set; }
        public string InstructorID { get; set; }

        public int NumberOfStudents { get; set; } = 0;
        public decimal Price { get; set; }
        public int CategoryID { get; set; }

        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }
        public bool IsBaned { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        public double Rating { get; set; }
        public int NumberOfRate { get; set; } = 0;

        public virtual Category Category { get; set; }
        public virtual Instructor Instructor { get; set; }

        public virtual IEnumerable<Assignment> Assignments { get; set; } = new List<Assignment>();
        public virtual IEnumerable<BookMark> BookMarks { get; set; } = new List<BookMark>();
        public virtual IEnumerable<Certificate> Certificates { get; set; } = new List<Certificate>();
        public virtual IEnumerable<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
        public virtual IEnumerable<Lecture> Lectures { get; set; } = new List<Lecture>();
        public virtual IEnumerable<Payment> Payments { get; set; } = new List<Payment>();
        public virtual IEnumerable<Review> Reviews { get; set; } = new List<Review>();
        public virtual IEnumerable<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}