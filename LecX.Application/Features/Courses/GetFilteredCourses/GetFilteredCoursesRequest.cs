using LecX.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.GetFilteredCourses
{
    public sealed class GetFilteredCoursesRequest : IRequest<GetFilteredCoursesResponse>
    {
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; }
        public CourseLevel? Level { get; set; }
        public CourseStatus? Status { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
