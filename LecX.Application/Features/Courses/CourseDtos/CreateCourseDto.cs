using LecX.Domain.Enums;

namespace LecX.Application.Features.Courses.CourseDtos
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = default!;
        public string CourseCode { get; set; } = default!; 
        public string? Description { get; set; }
        public string? CoverImagePath { get; set; }        
        public string InstructorId { get; set; } = default!;
        public int CategoryId { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus? Status { get; set; } 
        public decimal Price { get; set; }
        public DateTime? EndDate { get; set; }
    }
}