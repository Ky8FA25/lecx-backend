using LecX.Application.Common.Pagination;
using LecX.Application.Features.Courses.CourseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.GetAllCourses
{
    public sealed class GetAllCoursesRequest : IRequest<GetAllCoursesResponse>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
