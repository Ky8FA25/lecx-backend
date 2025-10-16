using AutoMapper;
using LecX.Application.Abstractions;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Pagination;
using LecX.Application.Features.Assignments.AssignmentsDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LecX.Application.Features.Assignments.GetAssignmentsByCourse
{
    public sealed class GetAssignmentsByCourseHandler(IAppDbContext db)
       : IRequestHandler<GetAssignmentsByCourseRequest, GetAssignmentsByCourseResponse>
    {
        

        public async Task<GetAssignmentsByCourseResponse> Handle(
            GetAssignmentsByCourseRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra Course có tồn tại không
                var courseExists = await db.Set<Course>()
                    .AnyAsync(c => c.CourseId == request.CourseId, cancellationToken);

                if (!courseExists)
                {
                    return new GetAssignmentsByCourseResponse(
                        false,
                        $"Course with ID {request.CourseId} not found"
                    );
                }

                // Xây dựng query
                var query = db.Set<Assignment>()
                    .Where(a => a.CourseId == request.CourseId)
                    .AsQueryable();

                // Lọc theo SearchWord nếu có
                if (!string.IsNullOrWhiteSpace(request.SearchWord))
                {
                    var searchTerm = request.SearchWord.Trim().ToLower();
                    query = query.Where(a =>
                        a.Title.ToLower().Contains(searchTerm) ||
                        a.AssignmentLink.ToLower().Contains(searchTerm)
                    );
                }

                // Lọc theo DateSearch (tìm assignments có DueDate >= DateSearch)
                if (request.DateSearch != default)
                {
                    query = query.Where(a => a.DueDate.Date >= request.DateSearch.Value.Date);
                }

                // Sắp xếp theo DueDate gần nhất trước
                query = query.OrderBy(a => a.DueDate);

                // Map sang DTO
                var mappedQuery = query.Select(a => new AssignmentDto(
                    a.AssignmentId,
                    a.CourseId,
                    a.Title,
                    a.StartDate,
                    a.DueDate,
                    a.AssignmentLink
                ));

                // Phân trang
                var paginatedList = await PaginatedList<AssignmentDto>.CreateAsync(
                    mappedQuery,
                    request.PageIndex,
                    request.PageSize,
                    cancellationToken
                );

                var message = paginatedList.TotalPages > 0
                ? "Assignments retrieved successfully"
                : "No assignments found for the specified criteria";
                return new GetAssignmentsByCourseResponse(
                    true,
                    message,
                    paginatedList
                );
            }
            catch (Exception ex)
            {
                return new GetAssignmentsByCourseResponse(
                    false,
                    $"An error occurred while retrieving assignments: {ex.Message}"
                );
            }
        }
    }
}

