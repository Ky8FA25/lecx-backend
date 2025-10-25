using FastEndpoints;
using LecX.Application.Features.Tests.TestHandler.DeleteTest;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.DeleteTest
{
    public sealed class DeleteTestEndpoint(ISender sender) : Endpoint<DeleteTestRequest, DeleteTestResponse>
    {
        public override void Configure()
        {
            Delete("/api/tests/{TestId:int}");
            Summary(s =>
            {
                s.Summary = "Delete a test";
                s.Description = "Deletes the test with the specified ID.";
            });
            Roles("Instructor", "Admin");
        }
        public override async Task HandleAsync(DeleteTestRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
