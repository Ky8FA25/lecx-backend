using AutoMapper;
using AutoMapper.QueryableExtensions;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Execption;
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
               .Where(c => c.CommentId == req.CommentId && !c.IsDeleted)
               .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
               .SingleOrDefaultAsync(ct);

            if (commentDto is null)
                throw new NotFoundException("Comment not found");

            return new GetCommentByIdResponse { Data = commentDto, Success = true };
        }
    }
}
