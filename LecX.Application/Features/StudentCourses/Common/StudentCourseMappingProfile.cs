using AutoMapper;
using LecX.Domain.Entities;

namespace LecX.Application.Features.StudentCourses.Common
{
    public class StudentCourseMappingProfile : Profile
    {
        public StudentCourseMappingProfile()
        {
            CreateMap<StudentCourse, StudentCourseDTO>().ReverseMap();
            CreateMap<User, StudentDTO>().ReverseMap();
        }
    }
}
