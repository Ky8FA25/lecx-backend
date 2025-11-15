using FastEndpoints;
using LecX.Application.Features.Tests.TestScoreHandler.CheckIsPassedTest;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Tests.Scores.CheckIsPassedTest
{
    public sealed class CheckIsPassedTestEndpoint(ISender sender, IHttpContextAccessor httpContext) : Endpoint<CheckIsPassedTestRequest,CheckIsPassedTestResponse>
    {
        public override void Configure()
        {
            Get("/api/tests/{TestId:int}/is-passed");
            Summary(s =>
            {
                s.Summary = "Check if a test is passed or not(for student)";
            });
        }
        public override async Task HandleAsync(CheckIsPassedTestRequest req, CancellationToken ct)
        {
            try
            {
                var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    await SendAsync(
                        new CheckIsPassedTestResponse { Message = "UserId not found", Success = false }, StatusCodes.Status400BadRequest, ct);
                    return;
                }
                req.StudentId = userId!;
                var response = await sender.Send(req, ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new CheckIsPassedTestResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
