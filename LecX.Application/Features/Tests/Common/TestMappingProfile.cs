using AutoMapper;
using LecX.Application.Features.Tests.TestHandler.CreateTest;
using LecX.Domain.Entities;

namespace LecX.Application.Features.Tests.Common
{
    public sealed class TestMappingProfile : Profile
    {
        public TestMappingProfile()
        {
            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<TestScore, TestScoreDTO>().ReverseMap();
            CreateMap<CreateTestRequest, Test>();
            CreateMap<Question, QuestionDTO>().ReverseMap();
        }
    }
}
