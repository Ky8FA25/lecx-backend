using FastEndpoints;
using LecX.Application.Features.Tests.QuestionHandler.CreateListQuestions;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Questions.CreateListQuestions
{
    public sealed class CreateListQuestionsEndpoint(ISender sender) : Endpoint<CreateListQuestionsRequest,CreateListQuestionsResponse>
    {
        public override void Configure()
        {
            Post("/api/tests/questions/lists");
            Summary(s =>
            {
                s.Summary = "Create a list questions for a test";
                s.Description = "Creates a list questions with the provided details for a test.";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(CreateListQuestionsRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
