using FastEndpoints;
using LecX.Application.Features.Tests.QuestionHandler.GetQuestionById;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.Questions.GetQuestionById
{
    public sealed class GetQuestionByIdEndpoint(ISender sender) : Endpoint<GetQuestionByIdRequest,GetQuestionByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/tests/questions/{QuestionId:int}");
            Summary(s =>
            {
                s.Summary = "Get a question detail by ID";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(GetQuestionByIdRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
