using MediatR;
using FastEndpoints;
using LecX.Application.Features.AssignmentScores.DeleteAssignmentScore;

namespace LecX.WebApi.Endpoints.AssignmentScores.DeleteAssignmentScore
{
    public class DeleteAssignmentScoreEndpoint (ISender sender):  Endpoint<DeleteAssignmentScoreRequest>
    {
        public override void Configure()
        {
            Delete("api/assignmentscores/{AssignmentScoreId}");
            Summary(s =>
            {
                s.Summary = "Delete Assignment Score by AssignmentScoreId";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(DeleteAssignmentScoreRequest req, CancellationToken ct)
        {
            await sender.Send(req, ct);
            await SendNoContentAsync(cancellation: ct);
        }
    }
    
    }

