using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Lectures.CreateLectureFile
{
    public sealed class CreateLectureFileHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<CreateLectureFileRequest, CreateLectureFileResponse>
    {
        public async Task<CreateLectureFileResponse> Handle(CreateLectureFileRequest request, CancellationToken ct)
        {
            try
            {
                var lectureFile = mapper.Map<LectureFile>(request);
                lectureFile.UploadDate = DateTime.Now;
                await db.Set<LectureFile>().AddAsync(lectureFile, ct);
                await db.SaveChangesAsync(ct);
                return new CreateLectureFileResponse(true, "Lecture file created successfully.", null);
            }
            catch (Exception ex)
            {
                return new CreateLectureFileResponse(false, $"Error creating lecture file: {ex.Message}", null);
            }
        }
    }
}
