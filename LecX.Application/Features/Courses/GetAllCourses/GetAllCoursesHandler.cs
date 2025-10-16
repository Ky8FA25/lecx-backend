using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Courses.GetAllCourses
{
    public sealed class GetAllCoursesHandler(IAppDbContext db, IMapper mapper)
       : IRequestHandler<GetAllCoursesRequest, GetAllCoursesResponse>
    {
        public async Task<GetAllCoursesResponse> Handle(GetAllCoursesRequest req, CancellationToken ct)
        {
            var query = db.Set<Course>()
                            .AsNoTracking()
                            .OrderByDescending(c => c.CreateDate);

            // phân trang entity
            var paginated = await PaginatedResponse<Course>.CreateAsync(query, req.PageIndex, req.PageSize, ct);

            // map sang DTO + giữ nguyên metadata
            var result = paginated.MapItems(c => mapper.Map<CourseDto>(c));

            return new GetAllCoursesResponse(result);
        }
    }
}
