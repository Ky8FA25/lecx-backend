using LecX.Application.Abstractions;
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
            var comment = await dbContext.Set<Comment>()
               .SingleOrDefaultAsync(c => c.CommentId == req.CommentId, ct);

            if (comment is null || comment.IsDeleted)
                throw new KeyNotFoundException("Comment not found");

            comment.IsDeleted = true;

            try
            {
                var affected = await dbContext.SaveChangesAsync(ct);
                return affected > 0
                    ? new() { Success = true, Message = "Deleted successfully" }
                    : new() { Success = false, Message = "No rows affected" };
            }
            catch (DbUpdateConcurrencyException)
            {
                return new() { Success = false, Message = "Comment no longer exists (concurrency)" };
            }
            catch (DbUpdateException)
            {
                return new() { Success = false, Message = "Database error while deleting comment" };
            }
        }
    }
}