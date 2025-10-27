using FastEndpoints;
using LecX.Application.Features.Tests.TestHandler.UpdateTest;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.UpdateTest
{
    public sealed class UpdateTestEndpoint(ISender sender) : Endpoint<UpdateTestRequest, UpdateTestResponse>
    {
        public override void Configure()
        {
            Put("/api/tests/{TestId:int}");
            Summary(s =>
            {
                s.Summary = "Update an existing test";
                s.Description = "Updates the details of an existing test with the provided information.";
            });
            Roles("Instructor", "Admin");
        }
        public override async Task HandleAsync(UpdateTestRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
