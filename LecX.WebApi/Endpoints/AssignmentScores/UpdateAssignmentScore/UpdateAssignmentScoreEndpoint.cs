using LecX.Domain.Enums;
using MediatR;
using FastEndpoints;
using LecX.Application.Features.AssignmentScores.UpdateAssignmentScore;

namespace LecX.WebApi.Endpoints.AssignmentScores.UpdateAssignmentScore
{
    public class UpdateAssignmentScoreEndpoint(ISender sender): Endpoint<UpdateAssignmentScoreRequest, UpdateAssignmentScoreResponse>
    {
        public override void Configure()
        {
            Put("api/assignmentscores/{AssignmentScoreId}");
            Summary(s =>
            {
                s.Summary = "Update Score by AssignmentScoreId";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(UpdateAssignmentScoreRequest req, CancellationToken ct)
        {
            
            var result =  await sender.Send(req, ct);
            await SendAsync(result, cancellation: ct);
        }
    }
    
    }

