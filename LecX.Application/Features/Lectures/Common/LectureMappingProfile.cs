using AutoMapper;
using LecX.Application.Features.Lectures.CreateLecture;
using LecX.Application.Features.Lectures.CreateLectureCompletion;
using LecX.Application.Features.Lectures.CreateLectureFile;
using LecX.Domain.Entities;
namespace LecX.Application.Features.Lectures.Common
{
    public sealed class LectureMappingProfile : Profile
    {
        public LectureMappingProfile()
        {
            CreateMap<Lecture, LectureDTO>();
            CreateMap<LectureFile, LectureFileDto>();
            CreateMap<CreateLectureRequest, Lecture>();
            CreateMap<CreateLectureFileRequest, LectureFile>();
            CreateMap<CreateLectureCompletionRequest, LectureCompletion>();
            CreateMap<LectureCompletion, LectureCompletionDTO>();
            CreateMap<User, StudentCompletedLectureDTO>();
        }
    }
}
