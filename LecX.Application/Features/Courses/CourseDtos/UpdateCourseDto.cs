using LecX.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.CourseDtos
{
    public class UpdateCourseDto
    {
        public string Title { get; set; } = default!;
        public string CourseCode { get; set; } = default!;
        public string? Description { get; set; }
        public string? CoverImagePath { get; set; }        
        public int CategoryId { get; set; }
        public CourseLevel Level { get; set; }        
        public decimal Price { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
