using FastEndpoints;
using LecX.Application.Features.Tests.TestHandler.GetTestById;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.GetTestById
{
    public sealed class GetTestByIdEndpoint(ISender sender) : Endpoint<GetTestByIdRequest, GetTestByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/tests/{TestId:int}");
            Summary(s =>
            {
                s.Summary = "Get test by ID";
                s.Description = "Retrieves the test details for the specified test ID.";
            });
            Roles("Instructor", "Admin", "Student");
        }
        public override async Task HandleAsync(GetTestByIdRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
