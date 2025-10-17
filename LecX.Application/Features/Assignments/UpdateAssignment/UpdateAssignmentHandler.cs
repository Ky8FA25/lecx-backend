using LecX.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LecX.Domain.Entities;
using LecX.Application.Abstractions.Persistence;

namespace LecX.Application.Features.Assignments.UpdateAssignment
{
    public  class UpdateAssignmentHandler(IAppDbContext db): IRequestHandler<UpdateAssignmentRequest, UpdateAssignmentResponse>
    {
        public async Task<UpdateAssignmentResponse> Handle(UpdateAssignmentRequest req, CancellationToken ct)
        {
            try
            {
                var entity = await db.Set<Assignment>().FindAsync(new object?[] { req.AssignmentId }, ct);
                if (entity == null)
                {
                    return new UpdateAssignmentResponse(false, "Assignment not found.");
                }
                entity.CourseId = req.CourseId;
                entity.Title = req.Title;
                entity.StartDate = req.StartDate;
                entity.DueDate = req.DueDate;
                entity.AssignmentLink = req.AssignmentLink;
                await db.SaveChangesAsync(ct);
                return new UpdateAssignmentResponse(true, "Assignment updated successfully.");
            }
            catch (Exception ex)
            {
                return new UpdateAssignmentResponse(false, $"Error updating assignment: {ex.Message}");
            }
        }
    }
    
    }

