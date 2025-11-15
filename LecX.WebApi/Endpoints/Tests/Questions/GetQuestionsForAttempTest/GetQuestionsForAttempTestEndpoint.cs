using FastEndpoints;
using LecX.Application.Features.Tests.QuestionHandler.GetQuestionsForAttempTest;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Questions.GetQuestionsForAttempTest
{
    public sealed class GetQuestionsForAttempTestEndpoint(ISender sender) : Endpoint<GetQuestionsForAttempTestRequest,GetQuestionsForAttempTestResponse>
    {
        public override void Configure()
        {
            Get("/api/tests/{TestId:int}/attempt/questions");
            Summary(s =>
            {
                s.Summary = "Get list questions in a test to student attempting test by TestId";
            });
        }
        public override async Task HandleAsync(GetQuestionsForAttempTestRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
