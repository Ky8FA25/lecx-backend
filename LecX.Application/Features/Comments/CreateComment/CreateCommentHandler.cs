using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Comments.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Comments.CreateComment
{
    public sealed class CreateCommentHandler(
     IMapper mapper,
     IAppDbContext db
 ) : IRequestHandler<CreateCommentRequest, CreateCommentResponse>
    {
        public async Task<CreateCommentResponse> Handle(CreateCommentRequest req, CancellationToken ct)
        {
            var lectureExists = await db.Set<Lecture>()
                .AnyAsync(x => x.LectureId == req.LectureId, ct);
            if (!lectureExists)
                return new("Lecture not found");

            if (req.ParentCmtId is not null)
            {
                var parent = await db.Set<Comment>()
                    .SingleOrDefaultAsync(c =>
                        c.LectureId == req.LectureId &&
                        c.CommentId == req.ParentCmtId, ct);

                if (parent is null || parent.IsDeleted)
                    return new("Parent comment invalid");
            }

            var comment = mapper.Map<Comment>(req);
            await db.Set<Comment>().AddAsync(comment, ct);

            try
            {
                var affected = await db.SaveChangesAsync(ct);

                return affected > 0
                    ? new("Success", true, mapper.Map<CommentDto>(comment))
                    : new("Failed");
            }
            catch (DbUpdateException)
            {
                return new("Error while creating comment");
            }
        }
    }
}
