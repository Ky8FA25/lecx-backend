using AutoMapper;
using AutoMapper.QueryableExtensions;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Comments.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Comments.GetCommentsByLecture
{
    public class GetCommentsByLectureHandler(
        IMapper mapper,
        IAppDbContext context
        ) : IRequestHandler<GetCommentsByLectureRequest, GetCommentsByLectureResponse>
    {

        /// <summary>
        /// Get by lecture id and parent comment id (null for root comments) with pagination
        /// </summary>
        public async Task<GetCommentsByLectureResponse> Handle(GetCommentsByLectureRequest req, CancellationToken ct)
        {
            var comments = context.Set<Comment>().AsNoTracking();

            IQueryable<Comment> baseQuery;

            if (req.ParentCmtId != null) // đang lấy replies
            {
                // 1) Check parent còn sống (tránh subquery trên từng row)
                var parentAlive = await comments.AnyAsync(p =>
                    p.CommentId == req.ParentCmtId &&
                    p.LectureId == req.LectureId &&
                    !p.IsDeleted, ct);

                if (!parentAlive)
                {
                    // parent đã xoá → trả trang rỗng (hoặc throw NotFound tuỳ rule)
                    var empty = new PaginatedList<CommentDto>(
                        new List<CommentDto>(), 0, req.PageIndex, req.PageSize);
                    return new() { Data = empty, Success = true };
                }
            }

            baseQuery = comments.Where(c =>
                c.LectureId == req.LectureId &&
                c.ParentCmtId == req.ParentCmtId &&
                !c.IsDeleted);

            var query = baseQuery
                .OrderByDescending(c => c.Timestamp).ThenBy(c => c.CommentId) // order ổn định cho paging
                .ProjectTo<CommentDto>(mapper.ConfigurationProvider);

            var page = await PaginatedList<CommentDto>.CreateAsync(query, req.PageIndex, req.PageSize, ct);
            return new() { Data = page, Success = true };
        }
    }
}
