using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.GetFilteredCourses
{
    public sealed class GetFilteredCoursesHandler(IAppDbContext db, IMapper mapper)
       : IRequestHandler<GetFilteredCoursesRequest, GetFilteredCoursesResponse>
    {
        public async Task<GetFilteredCoursesResponse> Handle(GetFilteredCoursesRequest req, CancellationToken ct)
        {
            var query = db.Set<Course>()
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .AsQueryable();

            // 🔹 Lọc theo Keyword
            if (!string.IsNullOrWhiteSpace(req.Keyword))
            {
                var kw = req.Keyword.Trim().ToLower();
                query = query.Where(c =>
                    c.Title.ToLower().Contains(kw) ||
                    c.CourseCode.ToLower().Contains(kw) ||
                    (c.Description != null && c.Description.ToLower().Contains(kw))
                );
            }

            // 🔹 Lọc theo Category
            if (req.CategoryId.HasValue && req.CategoryId.Value > 0)
                query = query.Where(c => c.CategoryId == req.CategoryId.Value);

            // 🔹 Lọc theo Level
            if (req.Level.HasValue)
                query = query.Where(c => c.Level == req.Level.Value);

            // 🔹 Lọc theo Status
            if (req.Status.HasValue)
                query = query.Where(c => c.Status == req.Status.Value);

            query = query.OrderByDescending(c => c.CreateDate);

            // 🔹 Phân trang + map DTO
            var paginated = await PaginatedResponse<Course>.CreateAsync(query, req.PageIndex, req.PageSize, ct);
            var result = paginated.MapItems(c => mapper.Map<CourseDto>(c));

            return new GetFilteredCoursesResponse(result);
        }
    }
}
