using LecX.Application.Common.Dtos;
using LecX.Application.Features.Courses.CourseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.GetFilteredCourses
{
    public sealed record GetFilteredCoursesResponse(PaginatedResponse<CourseDto> Data);
}
