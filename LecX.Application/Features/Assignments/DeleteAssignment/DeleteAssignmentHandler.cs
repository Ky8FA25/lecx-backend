using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Abstractions;
using MediatR;
using LecX.Domain.Entities;
using LecX.Application.Abstractions.Persistence;
namespace LecX.Application.Features.Assignments.DeleteAssignment
{
    public class DeleteAssignmentHandler(IAppDbContext db): IRequestHandler<DeleteAssignmentRequest , DeleteAssignmentResponse>
    {
        public async Task<DeleteAssignmentResponse> Handle(DeleteAssignmentRequest req, CancellationToken ct)
        {
            try
            {
                var entity = await db.Set<Assignment>().FindAsync(new object?[] { req.AssignmentId}, ct);
                if (entity == null)
                {
                    return new DeleteAssignmentResponse(false, "Assignment not found.");
                }
                db.Set<Assignment>().Remove(entity);
                await db.SaveChangesAsync(ct);
                return new DeleteAssignmentResponse(true, "Assignment deleted successfully.");
            }
            catch (Exception ex)
            {
                return new DeleteAssignmentResponse(false, $"Error deleting assignment: {ex.Message}");
            }
        }
        

    }
    
    }

