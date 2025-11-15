using FastEndpoints;
using LecX.Application.Features.Tests.QuestionHandler.CreateQuestion;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Questions.CreateQuestion
{
    public sealed class CreateQuestionEndpoint(ISender sender) : Endpoint<CreateQuestionRequest,CreateQuestionResponse>
    {
        public override void Configure()
        {
            Post("/api/tests/questions");
            Summary(s =>
            {
                s.Summary = "Create a new question for a test";
                s.Description = "Creates a new question with the provided details for a test.";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(CreateQuestionRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
