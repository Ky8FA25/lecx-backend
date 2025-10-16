using AutoMapper;
using LecX.Application.Features.Comments.Common;
using LecX.Application.Features.Comments.CreateComment;
using LecX.Domain.Entities;
using LecX.Application.Features.Submissions.SubmissionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Features.Submissions.CreateSubmission;

namespace LecX.Application.Features.Submissions.Common
{
    public class SubmissionMappingProfile : Profile
    {
        public SubmissionMappingProfile()
        {
            CreateMap<Submission, SubmissionDto>();
            CreateMap<User, SubmissionUserDto>();
            CreateMap<CreateSubmissionRequest, Submission>();


        }
    }

}
 