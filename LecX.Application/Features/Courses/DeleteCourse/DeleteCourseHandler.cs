using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.DeleteCourse
{
    public class DeleteCourseHandler(IAppDbContext db) : IRequestHandler<DeleteCourseRequest, DeleteCourseResponse>
    {
        public async Task<DeleteCourseResponse> Handle(DeleteCourseRequest request, CancellationToken cancellationToken)
        {
            var course = await db.Set<Course>().FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken);

            if (course == null)
            {
                               // Handle the case where the course is not found
                throw new KeyNotFoundException($"Course with ID '{request.CourseId}' not found.");
            }

            db.Set<Course>().Remove(course);
            await db.SaveChangesAsync(cancellationToken);

            return new DeleteCourseResponse(true, $"Course '{course.CourseId}' deleted successfully.");
        }
    }
}
