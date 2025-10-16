using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Pagination;
using LecX.Application.Features.Courses.GetFilteredCourses;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LecX.Application.Features.Submissions.SubmissionDtos;
namespace LecX.Application.Features.Submissions.GetSubmissionsByAssignment
{
    public sealed class GetSubmissionsByAssignmentHandler(IAppDbContext db, IMapper mapper)
       : IRequestHandler<GetSubmissionsByAssignmentRequest, GetSubmissionsByAssignmentResponse>
    {
        public async Task<GetSubmissionsByAssignmentResponse> Handle(GetSubmissionsByAssignmentRequest req, CancellationToken ct)
        {
            var query = db.Set<Submission>()
                .AsNoTracking()
                .AsQueryable();


            // 🔹 Lọc theo Assignment
            if (req.AssignmentId > 0 && req.AssignmentId != null)
                query = query.Where(c => c.AssignmentId == req.AssignmentId);

            query = query.OrderByDescending(c => c.SubmissionDate);

            // 🔹 Phân trang + map DTO
            var paginated = await PaginatedResponse<Submission>.CreateAsync(query, req.PageIndex, req.PageSize, ct);
            var result = paginated.MapItems(c => mapper.Map<SubmissionDto>(c));

            return new GetSubmissionsByAssignmentResponse(result);
        }
    }
}

