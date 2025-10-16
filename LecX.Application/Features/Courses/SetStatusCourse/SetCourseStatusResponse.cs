using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.SetStatusCourse
{
    public sealed record SetCourseStatusResponse(bool IsUpdated, string Message);
}
