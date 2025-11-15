using LecX.Domain.Entities;
using AutoMapper;
using LecX.Application.Features.InstructorConfirmations.CreateInstructorConfirmation;


namespace LecX.Application.Features.InstructorConfirmations.Common
{
    public class InstructorConfirmationMappingProfile: Profile
    {
        public InstructorConfirmationMappingProfile()
        {
            CreateMap<User, UserConfirmationDto>();
            
            CreateMap<InstructorConfirmation, InstructorConfirmationDto>()
                .ForMember(d => d.User, opt => opt.MapFrom(src => src.User));
            
            CreateMap<CreateInstructorConfirmationRequest, InstructorConfirmation>();
           
        }
    }
}
