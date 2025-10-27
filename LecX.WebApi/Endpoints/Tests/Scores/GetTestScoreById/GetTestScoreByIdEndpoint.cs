using FastEndpoints;
using LecX.Application.Features.Tests.TestScoreHandler.GetTestScoreById;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Scores.GetTestScoreById
{
    public sealed class GetTestScoreByIdEndpoint(ISender sender) : Endpoint<GetTestScoreByIdRequest,GetTestScoreByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/tests/scores/{TestScoreId:int}");
            Summary(s =>
            {
                s.Summary = "Get a testscore";
            });
        }
        public override async Task HandleAsync(GetTestScoreByIdRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
