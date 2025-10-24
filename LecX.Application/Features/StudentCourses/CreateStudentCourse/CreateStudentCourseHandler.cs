using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.StudentCourses.CreateStudentCourse
{
    public sealed class CreateStudentCourseHandler(IAppDbContext db) : IRequestHandler<CreateStudentCourseRequest, CreateStudentCourseResponse>
    {
        public async Task<CreateStudentCourseResponse> Handle(CreateStudentCourseRequest request, CancellationToken ct)
        {
            try
            {
                var existedStudentCourse = await db.Set<StudentCourse>()
                                            .FirstOrDefaultAsync(sc => sc.StudentId == request.StudentId
                                                                    && sc.CourseId == request.CourseId, ct);
                if (existedStudentCourse != null)
                {
                    return new CreateStudentCourseResponse
                    {
                        Success = false,
                        Message = "Student is already enrolled this course."
                    };
                }

                var studentCourse = new StudentCourse
                {
                    StudentId = request.StudentId,
                    CourseId = request.CourseId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 0,
                    CertificateStatus = CertificateStatus.Pending
                };
                await db.Set<StudentCourse>().AddAsync(studentCourse, ct);
                var course = await db.Set<Course>()
                        .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, ct);
                if (course != null)
                    course.NumberOfStudents += 1;

                await db.SaveChangesAsync(ct);
                return new CreateStudentCourseResponse
                {
                    Success = true,
                    Data = null,
                    Message = "Student course created successfully."
                };

            }
            catch (Exception ex)
            {
                return new CreateStudentCourseResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
