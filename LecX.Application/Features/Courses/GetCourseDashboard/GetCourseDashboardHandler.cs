using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.StudentCourses.Common;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Courses.GetCourseDashboard
{
    public sealed class GetCourseDashboardHandler(IAppDbContext db, IMapper mapper)
       : IRequestHandler<GetCourseDashboardRequest, GetCourseDashboardResponse>
    {
        public async Task<GetCourseDashboardResponse> Handle(GetCourseDashboardRequest req, CancellationToken ct)
        {
            // Kiểm tra course có tồn tại không
            var course = await db.Set<Course>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == req.CourseId, ct);

            if (course == null)
            {
                return new GetCourseDashboardResponse
                {
                    Success = false,
                    Message = "Course not found"
                };
            }

            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);
            var startOfDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            var endOfDay = startOfDay.AddDays(1);

            // Tính earningMonth: Tổng doanh thu trong tháng hiện tại
            var earningMonth = await db.Set<LecX.Domain.Entities.Payment>()
                .AsNoTracking()
                .Where(p => p.CourseId == req.CourseId &&
                           p.Status == PaymentStatus.Completed &&
                           p.PaymentDate >= startOfMonth &&
                           p.PaymentDate < endOfMonth)
                .SumAsync(p => p.Amount, ct);

            // Tính earningDay: Tổng doanh thu trong ngày hiện tại
            var earningDay = await db.Set<LecX.Domain.Entities.Payment>()
                .AsNoTracking()
                .Where(p => p.CourseId == req.CourseId &&
                           p.Status == PaymentStatus.Completed &&
                           p.PaymentDate >= startOfDay &&
                           p.PaymentDate < endOfDay)
                .SumAsync(p => p.Amount, ct);

            // Đếm số học sinh đã đăng ký
            var numStudent = await db.Set<StudentCourse>()
                .AsNoTracking()
                .CountAsync(sc => sc.CourseId == req.CourseId, ct);

            // Lấy danh sách học sinh
            var studentCourses = await db.Set<StudentCourse>()
                .AsNoTracking()
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .Where(sc => sc.CourseId == req.CourseId)
                .OrderByDescending(sc => sc.EnrollmentDate)
                .ToListAsync(ct);

            var listStudent = studentCourses.Select(sc => mapper.Map<StudentCourseDTO>(sc)).ToList();

            var dashboard = new CourseDashboardDto
            {
                EarningMonth = earningMonth,
                EarningDay = earningDay,
                NumStudent = numStudent,
                Rating = course.Rating,
                ListStudent = listStudent
            };

            return new GetCourseDashboardResponse
            {
                Success = true,
                Message = "Dashboard data retrieved successfully",
                Data = dashboard
            };
        }
    }
}

