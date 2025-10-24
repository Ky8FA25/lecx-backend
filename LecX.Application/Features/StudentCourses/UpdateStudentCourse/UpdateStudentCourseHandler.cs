using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.StudentCourses.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.StudentCourses.UpdateStudentCourse
{
    public sealed class UpdateStudentCourseHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<UpdateStudentCourseRequest, UpdateStudentCourseResponse>
    {
        public async Task<UpdateStudentCourseResponse> Handle(UpdateStudentCourseRequest request, CancellationToken ct)
        {
            try
            {
                var studentCourse = await db.Set<StudentCourse>()
                                            .FirstOrDefaultAsync(sc => sc.StudentId == request.StudentId
                                                                    && sc.CourseId == request.CourseId, ct);
                if (studentCourse == null)
                {
                    return new UpdateStudentCourseResponse
                    {
                        Success = false,
                        Message = "Student course not found."
                    };
                }
                if (request.Progress.HasValue)
                {
                    studentCourse.Progress = request.Progress.Value;
                }
                if (request.CertificateStatus.HasValue)
                {
                    studentCourse.CertificateStatus = request.CertificateStatus.Value;
                }
                if (request.CompletionDate.HasValue)
                {
                    studentCourse.CompletionDate = request.CompletionDate.Value;
                }
                db.Set<StudentCourse>().Update(studentCourse);
                await db.SaveChangesAsync(ct);
                return new UpdateStudentCourseResponse
                {
                    Success = true,
                    Data = mapper.Map<StudentCourseDTO>(studentCourse),
                    Message = "Update student course successfully!"
                };

            }
            catch (Exception ex)
            {
                return new UpdateStudentCourseResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
