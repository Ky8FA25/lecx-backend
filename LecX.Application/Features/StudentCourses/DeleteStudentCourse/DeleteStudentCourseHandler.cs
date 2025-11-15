using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.StudentCourses.DeleteStudentCourse
{
    public sealed class DeleteStudentCourseHandler(IAppDbContext db) : IRequestHandler<DeleteStudentCourseRequest, DeleteStudentCourseResponse>
    {
        public async Task<DeleteStudentCourseResponse> Handle(DeleteStudentCourseRequest request, CancellationToken ct)
        {
            try
            {
                var studentCourse = await db.Set<StudentCourse>()
                    .FindAsync([request.StudentCourseId], ct);
                if (studentCourse == null)
                {
                    return new DeleteStudentCourseResponse
                    {
                        Success = false,
                        Message = "Student course not found."
                    };
                }
                db.Set<StudentCourse>().Remove(studentCourse);
                var course = await db.Set<Course>()
                        .FirstOrDefaultAsync(c => c.CourseId == studentCourse.CourseId, ct);
                if (course != null)
                    course.NumberOfStudents -= 1;
                await db.SaveChangesAsync(ct);
                return new DeleteStudentCourseResponse
                {
                    Success = true,
                    Message = "Student course deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new DeleteStudentCourseResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
