using AutoMapper;
using LecX.Application.Common.Dtos;
using LecX.Domain.Entities;

namespace LecX.WebApi.ODataControllers
{
    public class ODataMappingProfile : Profile
    {
        public ODataMappingProfile()
        {
            CreateMap<User, UserODataDto>();

            CreateMap<Course, CourseODataDto>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.FullName : "null")
                )
                .ForMember(
                    dest => dest.InstructorName,
                    opt => opt.MapFrom(src =>
                        src.Instructor.User != null
                            ? $"{src.Instructor.User.FirstName} {src.Instructor.User.LastName}"
                            : null
                    )
                );

            CreateMap<Category, CategoryODataDto>();

            CreateMap<StudentCourse, StudentCourseODataDto>()
                .ForMember(
                    dest => dest.StudentName,
                    opt => opt.MapFrom(src =>
                        src.Student != null
                            ? $"{src.Student.FirstName} {src.Student.LastName}"
                            : null
                    )
                )
                .ForMember(
                    dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : "null")
                );

            CreateMap<Payment, PaymentODataDto>()
                .ForMember(
                    dest => dest.StudentName,
                    opt => opt.MapFrom(src =>
                        src.Student != null
                            ? $"{src.Student.FirstName} {src.Student.LastName}"
                            : null
                    )
                )
                .ForMember(
                    dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : "null")
                );

            CreateMap<Report, ReportODataDto>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src =>
                        src.User != null
                            ? $"{src.User.FirstName} {src.User.LastName}"
                            : null
                    )
                );
        }
    }
}
