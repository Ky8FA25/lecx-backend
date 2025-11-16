using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.AssignmentScores.Common;
using LecX.Application.Features.AssignmentScores.GetAssignmentScoreById;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreByCourseId
{
    internal class GetAssignmentScoreByCourseIdHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetAssignmentScoreByCourseIdRequest, GetAssignmentScoreByCourseIdResponse>
    {
        public async Task<GetAssignmentScoreByCourseIdResponse> Handle(GetAssignmentScoreByCourseIdRequest req, CancellationToken ct)
        {
            // Lấy tất cả AssignmentScore của course
            var scores = await db.Set<AssignmentScore>()
    .Include(x => x.Assignment)
    .Where(x => x.Assignment.CourseId == req.CourseId)
    .ToListAsync(ct);

            if (!scores.Any())
            {
                return new GetAssignmentScoreByCourseIdResponse(
                    false,
                    "No assignment scores found for this course",
                    null
                );
            }

            var dtos = mapper.Map<List<AssignmentScoreFullDataDto>>(scores);

            return new GetAssignmentScoreByCourseIdResponse(
                true,
                "Assignment scores retrieved successfully",
                dtos
            );
        }
    }
}
