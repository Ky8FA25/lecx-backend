using MediatR;

namespace LecX.Application.Features.Courses.GetCoursesByInstructorId
{
    public sealed class GetCoursesByInstructorIdRequest : IRequest<GetCoursesByInstructorIdResponse>
    {
        public string? InstructorId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}


