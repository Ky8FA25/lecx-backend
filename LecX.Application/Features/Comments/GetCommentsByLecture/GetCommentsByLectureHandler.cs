using AutoMapper;
using AutoMapper.QueryableExtensions;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Pagination;
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
            try
            {
                var query = context.Set<Comment>()
                  .AsNoTracking()
                  .Where(c =>
                         c.LectureId == req.LectureId &&
                         c.ParentCmtId == req.ParentCmtId && !c.IsDeleted)
                  .OrderBy(c => c.Timestamp)
                  .ProjectTo<CommentDto>(mapper.ConfigurationProvider);

                var page = await PaginatedList<CommentDto>.CreateAsync(
                    query, req.PageIndex, req.PageSize, ct);

                return new GetCommentsByLectureResponse { Data = page, Success = true };
            }
            catch (Exception ex)
            {
                return new() { Message = ex.Message, Success = false };
            }
        }
    }
}
