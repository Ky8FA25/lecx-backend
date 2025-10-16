using FastEndpoints;
using LecX.Application.Features.Assignments.DeleteAssignment;
using LecX.Application.Features.Comments.DeleteComment;
using LecX.Application.Features.Submissions.DeleteSubmission;
using MediatR;

namespace LecX.WebApi.Endpoints.Submissions.DeleteSubmission
{
    public class DeleteSubmissionEndpoint(ISender sender) : Endpoint<DeleteSubmissionRequest, DeleteSubmissionResponse>
    {
        public override void Configure()
        {
            Delete("/api/submissions/{submissionId}");
            Summary(s =>
            {
                s.Summary = "Delete an submission by ID";
            });
            Roles("Student", "Instructor");
        }

        public override async Task HandleAsync(DeleteSubmissionRequest req, CancellationToken ct)
        {
            var submissionId = Route<int>("submissionId");
            var response = await sender.Send(new DeleteSubmissionRequest(submissionId), ct);
            await SendAsync(response, cancellation: ct);

        }
    }
}
