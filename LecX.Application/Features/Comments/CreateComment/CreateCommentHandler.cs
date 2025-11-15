using AutoMapper;
using AutoMapper.QueryableExtensions;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Execption;
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
            if (string.IsNullOrWhiteSpace(req.UserId))
                throw new ForbiddenException("Unauthorized");

            var lectureExists = await db.Set<Lecture>()
                .AnyAsync(x => x.LectureId == req.LectureId, ct);
            if (!lectureExists)
                throw new NotFoundException("Lecture not found");

            if (req.ParentCmtId is not null)
            {
                var parent = await db.Set<Comment>()
                    .SingleOrDefaultAsync(c =>
                        c.LectureId == req.LectureId &&
                        c.CommentId == req.ParentCmtId, ct);

                if (parent is null || parent.IsDeleted)
                    throw new InvalidOperationException("Parent comment invalid");
            }

            var comment = mapper.Map<Comment>(req);
            await db.Set<Comment>().AddAsync(comment, ct);

            if (req.File is not null)
            {
                var commentFile = mapper.Map<CommentFile>(req.File);
                commentFile.Comment = comment;
                await db.Set<CommentFile>().AddAsync(commentFile, ct);
            }

            await db.SaveChangesAsync(ct);

            var created = await db.Set<Comment>()
                .AsNoTracking()
                .Where(c => c.CommentId == comment.CommentId)
                .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(ct);

            return new("Success", true, created);
        }
    }
}
