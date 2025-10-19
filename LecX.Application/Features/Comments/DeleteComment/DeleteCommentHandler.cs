using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Execption;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Comments.DeleteComment
{
    public sealed class DeleteCommentHandler(
        IAppDbContext dbContext
    ) : IRequestHandler<DeleteCommentRequest, DeleteCommentResponse>
    {
        public async Task<DeleteCommentResponse> Handle(DeleteCommentRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.UserId))
                throw new ForbiddenException("Unauthorized");

            var comment = await dbContext.Set<Comment>()
               .SingleOrDefaultAsync(c => c.CommentId == req.CommentId, ct);

            if (comment is null || comment.IsDeleted)
                throw new NotFoundException("Comment not found");

            if (comment.UserId != req.UserId)
                throw new ForbiddenException("You don't have permission to delete this comment");

            comment.IsDeleted = true;

            var affected = await dbContext.SaveChangesAsync(ct);
            return affected > 0
                ? new(true, "Deleted successfully")
                : new(false, "No rows affected");
        }
    }
}