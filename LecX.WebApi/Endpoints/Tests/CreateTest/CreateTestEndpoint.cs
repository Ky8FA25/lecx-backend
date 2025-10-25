using FastEndpoints;
using LecX.Application.Features.Tests.TestHandler.CreateTest;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.CreateTest
{
    public sealed class CreateTestEndpoint(ISender sender) : Endpoint<CreateTestRequest, CreateTestResponse>
    {
        public override void Configure()
        {
            Post("/api/tests");
            Summary(s =>
            {
                s.Summary = "Create a new test";
                s.Description = "Creates a new test with the provided details.";
            });
            Roles("Instructor", "Admin");
        }
        public override async Task HandleAsync(CreateTestRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation:ct);
        }
    }
}
