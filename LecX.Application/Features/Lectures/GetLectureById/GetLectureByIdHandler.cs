using AutoMapper;
using LecX.Application.Abstractions;
using LecX.Application.Features.Lectures.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Lectures.GetLectureById
{
    public class GetLectureByIdHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetLectureByIdRequest, GetLectureByIdResponse>
    {
        public async Task<GetLectureByIdResponse> Handle(GetLectureByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var lecture = await db.Set<Lecture>()
                                        .AsNoTracking()
                                        .AsSplitQuery()
                                        .Include(l => l.LectureFiles)
                                        .FirstOrDefaultAsync(l => l.LectureId == request.LectureId);
                if (lecture == null)
                {
                    return new GetLectureByIdResponse
                    {
                        Success = false,
                        Message = "Lecture not found."
                    };
                }
                var lectureDto = mapper.Map<LectureDTO>(lecture);
                return new GetLectureByIdResponse
                {
                    Success = true,
                    Message = "Lecture retrieved successfully.",
                    Data = lectureDto
                };

            }
            catch (Exception ex)
            {
                return new GetLectureByIdResponse
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}
