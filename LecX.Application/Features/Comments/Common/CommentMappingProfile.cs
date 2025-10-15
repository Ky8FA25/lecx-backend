using AutoMapper;
using LecX.Application.Features.Comments.CreateComment;
using LecX.Domain.Entities;

namespace LecX.Application.Features.Comments.Common
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<User, CommentUserDto>();
            CreateMap<CreateCommentRequest, Comment>();
            CreateMap<CommentFile, CommentFileDto>();
        }
    }
}