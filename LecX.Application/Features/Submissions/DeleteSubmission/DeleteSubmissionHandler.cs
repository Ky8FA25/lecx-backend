using LecX.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Submissions.DeleteSubmission;
using MediatR;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LecX.Application.Abstractions.Persistence;
namespace LecX.Application.Features.Submissions.DeleteSubmission
{
    public sealed class DeleteSubmissionHandler(IAppDbContext db) :IRequestHandler<DeleteSubmissionRequest, DeleteSubmissionResponse>
    {
        public async Task<DeleteSubmissionResponse> Handle(DeleteSubmissionRequest req, CancellationToken ct)
        {
            try
            {
                var submission = await db.Set<Submission>().SingleOrDefaultAsync(c => c.SubmissionId == req.SubmissionId, ct);
                if (submission == null)
                    throw new KeyNotFoundException("Submission not found");

                db.Set<Submission>().Remove(submission);
                await db.SaveChangesAsync(ct);
                return new DeleteSubmissionResponse(true, "Submission deleted successfully.");
            }
            catch(Exception ex) 
            {
                return new DeleteSubmissionResponse(false, $"Error deleting submission: {ex.Message}");
            }
            
        }
    }
}
