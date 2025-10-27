using FastEndpoints;
using LecX.Application.Features.Tests.TestScoreHandler.GetStudentScoresByTest;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Scores.GetStudentScoresByTest
{
    public sealed class GetStudentScoresByTestEndpoint(ISender sender) : Endpoint<GetStudentScoresByTestRequest,GetStudentScoresByTestResponse>
    {
        public override void Configure()
        {
            Get("/api/tests/{TestId:int}/scores");
            Summary(s =>
            {
                s.Summary = "Get all student scores in a test (for instructor)";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(GetStudentScoresByTestRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
