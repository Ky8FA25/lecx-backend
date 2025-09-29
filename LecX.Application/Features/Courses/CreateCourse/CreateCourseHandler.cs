using LecX.Application.Abstractions;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Courses.CreateCourse
{
    public sealed class CreateCourseHandler(IAppDbContext db)
        : IRequestHandler<CreateCourseRequest, CreateCourseResponse>
    {
        public async Task<CreateCourseResponse> Handle(CreateCourseRequest req, CancellationToken ct)
        {
            var entity = new Course { Title = req.Title, Price = req.Price };
            await db.Courses.AddAsync(entity, ct);
            await db.SaveChangesAsync(ct);

            var dto = new CourseDto(entity.CourseId, entity.Title, entity.Price);
            return new CreateCourseResponse(dto);
        }
    }
}
