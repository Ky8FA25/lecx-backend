using LecX.Application.Abstractions;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.SetStatusCourse
{
    public sealed class SetCourseStatusHandler(IAppDbContext db)
        : IRequestHandler<SetCourseStatusRequest, SetCourseStatusResponse>
    {
        public async Task<SetCourseStatusResponse> Handle(SetCourseStatusRequest req, CancellationToken ct)
        {
            var course = await db.Set<Course>()
                .FirstOrDefaultAsync(c => c.CourseId == req.CourseId, ct);

            if (course is null)
                return new SetCourseStatusResponse(false, $"Course with ID {req.CourseId} not found.");

            course.Status = req.Status;
            course.LastUpdate = DateTime.UtcNow;

            db.Set<Course>().Update(course);
            await db.SaveChangesAsync(ct);

            return new SetCourseStatusResponse(true,
                $"Course '{course.Title}' status updated to {course.Status}");
        }
    }
}
