using FastEndpoints;
using LecX.Application.Features.Tests.TestScoreHandler.CreateTestScore;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Tests.Scores.CreateTestScore
{
    public sealed class CreateTestScoreEndpoint(ISender sender, IHttpContextAccessor httpContext) : Endpoint<CreateTestScoreRequest, CreateTestScoreResponse>
    {
        public override void Configure()
        {
            Post("/api/tests/scores");
            Summary(s =>
            {
                s.Summary = "Submit a test answers to create a new test score for a test(for student)";
            });
        }
        public override async Task HandleAsync(CreateTestScoreRequest req, CancellationToken ct)
        {
            try
            {
                var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    await SendAsync(
                        new CreateTestScoreResponse { Message = "UserId not found", Success = false }, StatusCodes.Status400BadRequest, ct);
                    return;
                }
                req.StudentId = userId!;
                var response = await sender.Send(req, ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new CreateTestScoreResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
