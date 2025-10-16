using LecX.Application.Features.Courses.CourseDtos;
using MediatR;

namespace LecX.Application.Features.Courses.CreateCourse
{
    public sealed record CreateCourseRequest(CreateCourseDto CreateCourseDto)
        : IRequest<CreateCourseResponse>;
}
