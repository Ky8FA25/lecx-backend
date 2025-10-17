using MediatR;
using LecX.Application.Features.Submissions.UpdateSubmission;
using FastEndpoints;

namespace LecX.WebApi.Endpoints.Submissions.UpdateSubmission
{
    public class UpdateSubmissionEndpoint(ISender sender) : Endpoint<UpdateSubmissionRequest, UpdateSubmissionResponse>
    {
        public override void Configure()
        {
            Put("/api/submissions/{submissionId}");
            Summary(s =>
            {
                s.Summary = "Update an submission by ID";
            });
            Roles("Student", "Instructor");
        }
        public override async Task HandleAsync(UpdateSubmissionRequest req, CancellationToken ct)
        {
            var submissionId = Route<int>("submissionId");
            var response = await sender.Send(new UpdateSubmissionRequest(submissionId, req.FileName, req.SubmissionLink), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
