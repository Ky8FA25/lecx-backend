using AutoMapper;
using LecX.Application.Abstractions;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LecX.Application.Features.Courses.CreateCourse
{
    public sealed class CreateCourseHandler(IAppDbContext db, IMapper mapper)
        : IRequestHandler<CreateCourseRequest, CreateCourseResponse>
    {
        public async Task<CreateCourseResponse> Handle(CreateCourseRequest req, CancellationToken ct)
        {
           var dto = req.CreateCourseDto;

            var entity = mapper.Map<Course>(dto);
            
            entity.CreateDate = DateTime.UtcNow;
            entity.LastUpdate = DateTime.UtcNow;
            entity.Status = CourseStatus.Inactive; // New courses start as Inactive

            await db.Set<Course>().AddAsync(entity, ct);
            await db.SaveChangesAsync(ct);

            var CreatecourseDto = mapper.Map<CreateCourseDto>(entity);

            return new CreateCourseResponse(CreatecourseDto);
        }
    }
}
