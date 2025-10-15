using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.CourseDtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string CourseCode { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string InstructorId { get; set; } = default!;
        public int CategoryId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Level { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
