using AutoMapper;
using LecX.Application.Features.Comments.CreateComment;
using LecX.Domain.Entities;

namespace LecX.Application.Features.Comments.Common
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<User, CommentUserDto>();

            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.User, opt => opt.MapFrom(src => src.User))
                .ForMember(d => d.File, opt => opt.MapFrom(src =>
                    src.CommentFiles.FirstOrDefault()));
            CreateMap<CreateCommentRequest, Comment>();

            CreateMap<CommentFile, CommentFileDto>();
            CreateMap<CreateCommentFileRequest, CommentFile>();
        }
    }
}