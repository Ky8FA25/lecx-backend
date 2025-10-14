using AutoMapper;
using AutoMapper.QueryableExtensions;
using LecX.Application.Abstractions;
using LecX.Application.Features.Comments.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Comments.GetCommentById
{
    public sealed class GetCommentByIdHandler(
        IAppDbContext dbContext,
        IMapper mapper
    ) : IRequestHandler<GetCommentByIdRequest, GetCommentByIdResponse>
    {
        public async Task<GetCommentByIdResponse> Handle(GetCommentByIdRequest req, CancellationToken ct)
        {
            var commentDto = await dbContext.Set<Comment>()
               .AsNoTracking()
               .Where(c => c.CommentId == req.CommentId)
               .ProjectTo<CommentDto>(mapper.ConfigurationProvider) 
               .SingleOrDefaultAsync(ct);

            if (commentDto is null)
                throw new KeyNotFoundException("Comment not found");

            return new GetCommentByIdResponse { Comment = commentDto, Success = true };
        }
    }
}
