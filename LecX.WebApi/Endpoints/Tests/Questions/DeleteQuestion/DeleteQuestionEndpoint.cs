using FastEndpoints;
using LecX.Application.Features.Tests.QuestionHandler.DeleteQuestion;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Questions.DeleteQuestion
{
    public sealed class DeleteQuestionEndpoint(ISender sender) : Endpoint<DeleteQuestionRequest,DeleteQuestionResponse>
    {
        public override void Configure()
        {
            Delete("/api/tests/questions/{QuestionId:int}");
            Summary(s =>
            {
                s.Summary = "Delete a question in a test";
                s.Description = "Delete a question in a test.";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(DeleteQuestionRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
