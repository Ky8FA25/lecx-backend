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

namespace LecX.Application.Features.Courses.GetCourseById
{
    public sealed class GetCourseByIdHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetCourseByIdRequest, GetCourseByIdResponse>
    {
        public async Task<GetCourseByIdResponse> Handle(GetCourseByIdRequest request, CancellationToken cancellationToken)
        {
            var course = await db.Set<Course>()
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken);

            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {request.CourseId} not found.");
            }

            var courseDto = mapper.Map<CourseDto>(course);

            return new GetCourseByIdResponse(courseDto);
        }
    }
}
