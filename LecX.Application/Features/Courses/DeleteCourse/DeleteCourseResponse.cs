using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.DeleteCourse
{
    public sealed record DeleteCourseResponse(bool IsDeleted, string Message)
    {
    }
}
