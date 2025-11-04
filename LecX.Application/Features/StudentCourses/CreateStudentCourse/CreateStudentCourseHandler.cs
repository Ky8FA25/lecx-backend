using LecX.Application.Abstractions.ExternalServices.Mail;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LecX.Application.Features.StudentCourses.CreateStudentCourse
{
    public sealed class CreateStudentCourseHandler(
        IAppDbContext db,
        IMailTemplateService mailTpl,
        IMailService mail,
        IConfiguration config
    ) : IRequestHandler<CreateStudentCourseRequest, CreateStudentCourseResponse>
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

                var student = await db.Set<User>()
                         .FirstOrDefaultAsync(u => u.Id == studentCourse.StudentId, ct);

                if (course != null && student != null)
                {
                    var feBaseUrl = (config["Frontend:BaseUrl"] ?? string.Empty).TrimEnd('/');

                    var emailBody = await mailTpl.BuildWelcomeToCourseEmailAsync(
                        studentName: student.FirstName + student.LastName,
                        courseName: course.Title,
                        courseUrl: $"{feBaseUrl}/course/{course?.CourseId}",
                        email: student?.Email!
                    );

                    await mail.SendMailAsync(new MailContent
                    {
                        To = student?.Email!,
                        Subject = $"You’re enrolled! Welcome to {course?.Title}",
                        Body = emailBody
                    });
                }

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
