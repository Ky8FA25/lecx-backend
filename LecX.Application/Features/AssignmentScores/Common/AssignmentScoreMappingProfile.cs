using AutoMapper;
using LecX.Application.Features.AssignmentScores.CreateAssignmentScore;
using LecX.Application.Features.AssignmentScores.UpdateAssignmentScore;
using LecX.Domain.Entities;


namespace LecX.Application.Features.AssignmentScores.Common
{
    public class AssignmentScoreMappingProfile :Profile
    {
        public AssignmentScoreMappingProfile()
        {
            CreateMap<AssignmentScore, AssignmentScoreDto>();
            
            CreateMap<CreateAssignmentScoreRequest, AssignmentScore>();

            CreateMap<UpdateAssignmentScoreRequest, AssignmentScore>();
           
        }
    }
}
