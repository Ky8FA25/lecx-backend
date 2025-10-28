using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.StudentCourses.GetStudentCourseByStudent
{
    internal class GetStudentCourseByStudentHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetStudentCourseByStudentRequest, GetStudentCourseByStudentResponse>
    {
        public async Task<GetStudentCourseByStudentResponse> Handle(GetStudentCourseByStudentRequest request, CancellationToken cancellationToken)
        {
            var studentCourse = await db.Set<StudentCourse>().AsNoTracking()
                .Include(sc => sc.Course)
                .FirstOrDefaultAsync(sc =>
                    sc.StudentId == request.StudentId &&
                    sc.CourseId == request.CourseId,
                    cancellationToken);

            if (studentCourse == null)
            {
                return new GetStudentCourseByStudentResponse
                {
                    IsEnrolled = false
                };
            }

            // Đã mua khóa học
            return new GetStudentCourseByStudentResponse
            {
                IsEnrolled = true
            };
        }
    }
}

