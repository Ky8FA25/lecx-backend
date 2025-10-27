using FastEndpoints;
using LecX.Application.Features.Tests.TestScoreHandler.GetTestScoresByUser;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Tests.Scores.GetTestScoresByUser
{
    public sealed class GetTestScoresByUserEndpoint(ISender sender, IHttpContextAccessor httpContext) : Endpoint<GetTestScoresByUserRequest,GetTestScoresByUserResponse>
    {
        public override void Configure()
        {
            Get("/api/tests/scores");
            Summary(s =>
            {
                s.Summary = "Get scores for a test or all tests in a course";
            });
        }
        public override async Task HandleAsync(GetTestScoresByUserRequest req, CancellationToken ct)
        {
            try
            {
                var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    await SendAsync(
                        new GetTestScoresByUserResponse { Message = "UserId not found", Success = false }, StatusCodes.Status400BadRequest, ct);
                    return;
                }
                req.StudentId = userId!;
                var response = await sender.Send(req, ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new GetTestScoresByUserResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
