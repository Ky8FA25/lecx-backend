using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Execption;
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
            if (string.IsNullOrWhiteSpace(req.UserId))
                throw new ForbiddenException("Unauthorized");

            var comment = await db.Set<Comment>()
                .Where(c => c.CommentId == req.CommentId)
                .Where(c => !c.IsDeleted) // bản thân chưa bị xoá
                .SingleOrDefaultAsync(ct);

            if (comment is null)
                throw new NotFoundException("Comment not found");

            if (comment.UserId != req.UserId)
                throw new ForbiddenException("You don't have permission to edit this comment");

            comment.Content = req.Content;
            comment.IsEdited = true;
            await db.SaveChangesAsync(ct);

            var updated = mapper.Map<CommentDto>(comment);
            return new("Success", true, updated);
        }
    }
}

