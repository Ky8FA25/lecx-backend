using MediatR;

namespace LecX.Application.Features.Courses.GetAllCourses
{
    public sealed class GetAllCoursesRequest : IRequest<GetAllCoursesResponse>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
