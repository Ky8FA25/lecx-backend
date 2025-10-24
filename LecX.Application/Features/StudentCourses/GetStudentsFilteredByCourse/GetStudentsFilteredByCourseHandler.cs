using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.StudentCourses.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.StudentCourses.GetStudentsFilteredByCourse
{
    public sealed class GetStudentsFilteredByCourseHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetStudentsFilteredByCourseRequest, GetStudentsFilteredByCourseResponse>
    {
        public async Task<GetStudentsFilteredByCourseResponse> Handle(GetStudentsFilteredByCourseRequest request, CancellationToken ct)
        {
            try
            {
                var query = db.Set<StudentCourse>().AsNoTracking()
                .Where(c => c.CourseId == request.CourseId)
                .Include(c => c.Student)
                .AsQueryable();

                // 🔹 Lọc theo Keyword
                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    var kw = request.Keyword.Trim().ToLower();
                    query = query.Where(c =>
                        c.Student.FirstName.ToLower().Contains(kw) ||
                        (c.Student.LastName != null && c.Student.LastName.ToLower().Contains(kw))
                    );
                }
                // 🔹 Lọc theo CertificateStatus
                if (request.CertificateStatus.HasValue)
                    query = query.Where(c => c.CertificateStatus == request.CertificateStatus);

                query = query.OrderByDescending(c => c.Progress);
                // 🔹 Phân trang + map DTO
                var paginated = await PaginatedResponse<StudentCourse>.CreateAsync(query, request.PageIndex, request.PageSize, ct);
                var result = paginated.MapItems(c => mapper.Map<StudentCourseDTO>(c));
                return new GetStudentsFilteredByCourseResponse
                {
                    Success = true,
                    Data = result,
                    Message = "Courses retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new GetStudentsFilteredByCourseResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
