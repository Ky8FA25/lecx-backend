using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Courses.GetCoursesByInstructorId
{
    public sealed class GetCoursesByInstructorIdHandler(IAppDbContext db, IMapper mapper)
       : IRequestHandler<GetCoursesByInstructorIdRequest, GetCoursesByInstructorIdResponse>
    {
        public async Task<GetCoursesByInstructorIdResponse> Handle(GetCoursesByInstructorIdRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.InstructorId))
            {
                throw new ArgumentException("InstructorId is required", nameof(req.InstructorId));
            }

            var query = db.Set<Course>()
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .Where(c => c.InstructorId == req.InstructorId)
                .OrderByDescending(c => c.CreateDate);

            // Phân trang + map DTO
            var paginated = await PaginatedResponse<Course>.CreateAsync(query, req.PageIndex, req.PageSize, ct);
            var result = paginated.MapItems(c => mapper.Map<CourseDto>(c));

            return new GetCoursesByInstructorIdResponse(result);
        }
    }
}


