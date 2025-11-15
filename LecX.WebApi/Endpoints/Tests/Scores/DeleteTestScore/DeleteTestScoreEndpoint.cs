using FastEndpoints;
using LecX.Application.Features.Tests.TestScoreHandler.DeleteTestScore;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Scores.DeleteTestScore
{
    public sealed class DeleteTestScoreEndpoint(ISender sender) : Endpoint<DeleteTestScoreRequest,DeleteTestScoreResponse>
    {
        public override void Configure()
        {
            Delete("/api/tests/scores/{TestScoreId:int}");
            Summary(s =>
            {
                s.Summary = "Delete a testscore(for instructor and admin)";
            });
            Roles("Instructor", "Admin");
        }
        public override async Task HandleAsync(DeleteTestScoreRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
