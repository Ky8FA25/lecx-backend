using FastEndpoints;
using LecX.Application.Features.Tests.QuestionHandler.UpdateQuestion;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Questions.UpdateQuestion
{
    public sealed class UpdateQuestionEndpoint(ISender sender) : Endpoint<UpdateQuestionRequest,UpdateQuestionResponse>
    {
        public override void Configure()
        {
            Put("/api/tests/questions");
            Summary(s =>
            {
                s.Summary = "Update a question for a test";
                s.Description = "Update a question with the provided details for a test.";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(UpdateQuestionRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
