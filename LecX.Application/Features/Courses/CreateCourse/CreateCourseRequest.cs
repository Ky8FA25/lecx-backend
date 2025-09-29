using MediatR;

namespace LecX.Application.Features.Courses.CreateCourse
{
    public sealed record CreateCourseRequest(string Title, decimal Price)
        : IRequest<CreateCourseResponse>;
}
