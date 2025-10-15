using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.GetCourseById
{
    public sealed record GetCourseByIdRequest(int CourseId) : IRequest<GetCourseByIdResponse>
    {
    }
}
