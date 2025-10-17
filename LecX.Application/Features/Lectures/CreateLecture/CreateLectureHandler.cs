
using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Lectures.Common;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Lectures.CreateLecture
{
    public sealed class CreateLectureHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<CreateLectureRequest, CreateLectureResponse>
    {
        public async Task<CreateLectureResponse> Handle(CreateLectureRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var lecture = mapper.Map<Lecture>(request);
                await db.Set<Lecture>().AddAsync(lecture);
                await db.SaveChangesAsync(cancellationToken);
                var lectureDto = mapper.Map<LectureDTO>(lecture);
                return new CreateLectureResponse(true, "Lecture created successfully", lectureDto);
            }
            catch (Exception ex)
            {
                return new CreateLectureResponse(false, $"Error creating lecture: {ex.Message}", null);

            }
        }
    }
}
