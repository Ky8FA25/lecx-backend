using FastEndpoints;
using LecX.Application.Features.Tests.QuestionHandler.GetQuestionsByTest;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Questions.GetQuestionsByTest
{
    public sealed class GetQuestionsByTestEndpoint(ISender sender) : Endpoint<GetQuestionsByTestRequest,GetQuestionsByTestResponse>
    {
        public override void Configure()
        {
            Get("/api/tests/{TestId:int}/questions");
            Summary(s =>
            {
                s.Summary = "Get list questions paginated in a test by TestId";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(GetQuestionsByTestRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
