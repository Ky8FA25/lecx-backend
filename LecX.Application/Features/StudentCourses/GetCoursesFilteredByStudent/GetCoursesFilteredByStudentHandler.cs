using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.StudentCourses.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.StudentCourses.GetCoursesFilteredByStudent
{
    public sealed class GetCoursesFilteredByStudentHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetCoursesFilteredByStudentRequest, GetCoursesFilteredByStudentResponse>
    {
        public async Task<GetCoursesFilteredByStudentResponse> Handle(GetCoursesFilteredByStudentRequest request, CancellationToken ct)
        {
            try
            {
                var query = db.Set<StudentCourse>().AsNoTracking()
                .Where(c => c.StudentId == request.StudentId)
                .Include(c => c.Course)
                .AsQueryable();

                // 🔹 Lọc theo Keyword
                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    var kw = request.Keyword.Trim().ToLower();
                    query = query.Where(c =>
                        c.Course.Title.ToLower().Contains(kw) ||
                        c.Course.CourseCode.ToLower().Contains(kw) ||
                        (c.Course.Description != null && c.Course.Description.ToLower().Contains(kw))
                    );
                }

                // 🔹 Lọc theo Category
                if (request.CategoryId.HasValue && request.CategoryId > 0)
                    query = query.Where(c => c.Course.CategoryId == request.CategoryId);

                // 🔹 Lọc theo CertificateStatus
                if (request.CertificateStatus.HasValue)
                    query = query.Where(c => c.CertificateStatus == request.CertificateStatus);

                query = query.OrderByDescending(c => c.EnrollmentDate);
                // 🔹 Phân trang + map DTO
                var paginated = await PaginatedResponse<StudentCourse>.CreateAsync(query, request.PageIndex, request.PageSize, ct);
                var result = paginated.MapItems(c => mapper.Map<StudentCourseDTO>(c));
                return new GetCoursesFilteredByStudentResponse
                {
                    Success = true,
                    Data = result,
                    Message = "Courses retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new GetCoursesFilteredByStudentResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
