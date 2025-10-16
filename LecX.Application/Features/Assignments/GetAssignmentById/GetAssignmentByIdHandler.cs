using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LecX.Application.Features.Assignments.GetAssignmentById;
using LecX.Application.Abstractions;
using LecX.Domain.Entities;
using LecX.Application.Abstractions.Persistence;
namespace LecX.Application.Features.Assignments.GetAssignmentById
{
    public class GetAssignmentByIdHandler(IAppDbContext db) : IRequestHandler<GetAssignmentByIdRequest, GetAssignmentByIdResponse>
    {
        public Task<GetAssignmentByIdResponse> Handle(GetAssignmentByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var assignment = db.Set<Assignment>()
                   .FirstOrDefault(a => a.AssignmentId == request.AssignmentId);
                if (assignment == null)
                {
                    return Task.FromResult(new GetAssignmentByIdResponse(false, "Cannot find AssignmentId"));
                }
                var dto = new AssignmentsDtos.AssignmentDto(
                    assignment.AssignmentId,
                    assignment.CourseId,
                    assignment.Title,
                    assignment.StartDate,
                    assignment.DueDate,
                    assignment.AssignmentLink
                );
                return Task.FromResult(new GetAssignmentByIdResponse(true, "Assignment retrieved successfully.", dto));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetAssignmentByIdResponse(false, $"Error retrieving assignment: {ex.Message}"));

            }
        }
    }
}
