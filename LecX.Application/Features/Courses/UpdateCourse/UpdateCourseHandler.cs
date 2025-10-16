using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.UpdateCourse
{
    public sealed class UpdateCourseHandler(IAppDbContext db, IMapper mapper)
       : IRequestHandler<UpdateCourseRequest, UpdateCourseResponse>
    {
        public async Task<UpdateCourseResponse> Handle(UpdateCourseRequest req, CancellationToken ct)
        {
            var entity = await db.Set<Course>()
                .FirstOrDefaultAsync(c => c.CourseId == req.CourseId, ct);

            if (entity is null)
                throw new KeyNotFoundException($"Course with ID {req.CourseId} not found.");

            // Map thông tin mới từ DTO vào entity hiện có
            mapper.Map(req.UpdateCourseDto, entity);

            entity.LastUpdate = DateTime.UtcNow;

            db.Set<Course>().Update(entity);
            await db.SaveChangesAsync(ct);

            var updatedDto = mapper.Map<UpdateCourseDto>(entity);
            return new UpdateCourseResponse(updatedDto);
        }
    }
}
