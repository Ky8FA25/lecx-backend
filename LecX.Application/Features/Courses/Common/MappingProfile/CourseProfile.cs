using AutoMapper;
using LecX.Application.Features.Courses.CourseDtos;
using LecX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Courses.Common.MappingProfile
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            // CreateCourseDto → Course
            CreateMap<CreateCourseDto, Course>();

            // Course → CreateCourseDto
            CreateMap<Course, CreateCourseDto>();

            // Course → CourseDto
            CreateMap<Course, CourseDto>();

            // UpdateCourseDto → Course
            CreateMap<UpdateCourseDto, Course>()
                .ForMember(dest => dest.CourseId, opt => opt.Ignore())
                .ForMember(dest => dest.InstructorId, opt => opt.Ignore());
            // Course → UpdateCourseDto
            CreateMap<Course, UpdateCourseDto>();
        }
    }
}
