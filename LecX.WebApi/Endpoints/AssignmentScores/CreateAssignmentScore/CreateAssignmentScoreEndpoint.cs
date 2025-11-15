using FastEndpoints;
using LecX.Application.Features.Assignments.CreateAssignment;
using LecX.Application.Features.AssignmentScores.CreateAssignmentScore;
using MediatR;

namespace LecX.WebApi.Endpoints.AssignmentScores.CreateAssignmentScore
{
    public class CreateAssignmentScoreEndpoint (ISender sender) : Endpoint<CreateAssignmentScoreRequest , CreateAssignmentScoreResponse>
    {
        public override void Configure()
        {
            Post("/api/assignmentscores");
            Summary(s =>
            {
                s.Summary = "Create assignment score";
            });
           Roles("Instructor");
        }

        public override async Task HandleAsync(CreateAssignmentScoreRequest req, CancellationToken ct)
        {
            var res = await sender.Send(req, ct);
            await SendAsync(res, cancellation: ct);
        }
    }
}
