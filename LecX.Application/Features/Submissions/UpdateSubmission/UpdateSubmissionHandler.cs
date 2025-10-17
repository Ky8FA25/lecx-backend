using LecX.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Domain.Entities;
using LecX.Application.Abstractions.Persistence;
namespace LecX.Application.Features.Submissions.UpdateSubmission
{
    public sealed class UpdateSubmissionHandler(IAppDbContext db) : IRequestHandler<UpdateSubmissionRequest, UpdateSubmissionResponse>
    {
        public async Task<UpdateSubmissionResponse> Handle(UpdateSubmissionRequest request, CancellationToken ct)
        {
            try
            {
                var submission = await db.Set<Submission>().FindAsync(request.SubmissionId , ct);
                if (submission == null)
                {
                    return new UpdateSubmissionResponse(false, "Submission not found.");
                }
                if (!string.IsNullOrEmpty(request.FileName))
                    submission.FileName = request.FileName;
                if (!string.IsNullOrEmpty(request.SubmissionLink))
                    submission.SubmissionLink = request.SubmissionLink;
                db.Set<Submission>().Update(submission);
                await db.SaveChangesAsync(ct);
                return new UpdateSubmissionResponse(true, "Submission updated successfully.");
            }
            catch (Exception ex)
            {
                return new UpdateSubmissionResponse(false, $"Error updating submission: {ex.Message}");
            }
        }
    }
}
