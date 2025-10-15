using LecX.Application.Features.Courses.CourseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.UpdateCourse
{
    public sealed record UpdateCourseResponse(UpdateCourseDto UpdatedCourse);
    
}
