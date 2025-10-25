using FastEndpoints;
using LecX.Application.Features.Tests.TestHandler.GetTestsByCourse;
using MediatR;

namespace LecX.WebApi.Endpoints.Tests.GetTestsByCourse
{
    public sealed class GetTestsByCourseEndpoint(ISender sender) : Endpoint<GetTestsByCourseRequest, GetTestsByCourseResponse>
    {
        public override void Configure()
        {
            Get("/api/tests");
            Summary(s =>
            {
                s.Summary = "Get tests by course ID with filtering";
                s.Description = "Retrieves all tests associated with the specified course ID.";
            });
        }
        public override async Task HandleAsync(GetTestsByCourseRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
