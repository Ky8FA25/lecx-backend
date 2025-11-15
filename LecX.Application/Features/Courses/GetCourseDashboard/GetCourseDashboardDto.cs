using LecX.Application.Features.StudentCourses.Common;
using LecX.Domain.Enums;

namespace LecX.Application.Features.Courses.GetCourseDashboard
{
    public sealed class CourseDashboardDto
    {
        public decimal EarningMonth { get; set; }
        public decimal EarningDay { get; set; }
        public int NumStudent { get; set; }
        public double Rating { get; set; }
        public List<StudentCourseDTO> ListStudent { get; set; } = new List<StudentCourseDTO>();
    }
}

