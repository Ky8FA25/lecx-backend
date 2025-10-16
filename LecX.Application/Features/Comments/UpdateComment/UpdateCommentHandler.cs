using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Comments.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Comments.UpdateComment
{
    public sealed class UpdateCommentHandler(
        IMapper mapper,
        IAppDbContext db
    ) : IRequestHandler<UpdateCommentRequest, UpdateCommentResponse>
    {
        public async Task<UpdateCommentResponse> Handle(UpdateCommentRequest req, CancellationToken ct)
        {
            var comment = await db.Set<Comment>()
                .SingleOrDefaultAsync(c => c.CommentId == req.CommentId, ct);

            if (comment is null || comment.IsDeleted)
                throw new KeyNotFoundException("Comment not found");

            comment.Content = req.Content;
            comment.IsEdited = true;

            try
            {
                var affected = await db.SaveChangesAsync(ct);

                return affected > 0
                    ? new()
                    {
                        Data = mapper.Map<CommentDto>(comment),
                        Success = true,
                        Message = "Success"
                    }
                    : new() { Message = "Failed" };
            }
            catch (DbUpdateException)
            {
                return new() { Message = "Error while creating comment" };
            }
        }
    }
}

