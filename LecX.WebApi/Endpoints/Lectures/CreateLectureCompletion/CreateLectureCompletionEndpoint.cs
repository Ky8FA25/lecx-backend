using FastEndpoints;
using LecX.Application.Features.Lectures.CreateLectureCompletion;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Lectures.CreateLectureCompletion
{
    public class CreateLectureCompletionEndpoint(ISender sender, IHttpContextAccessor httpContext) : EndpointWithoutRequest<CreateLectureCompletionResponse>
    {
        public override void Configure()
        {
            Post("/api/lectures/completed/{lectureId}");
            Summary(s => s.Summary = "Create a record for completing lecture of a course (create by student)");
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            try
            {
                var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    await SendAsync(
                        new CreateLectureCompletionResponse { Message = "UserId not found", Success = false }, StatusCodes.Status400BadRequest, ct);
                    return;
                }
                var lectureId = Route<int>("lectureId");
                var response = await sender.Send(new CreateLectureCompletionRequest(lectureId,userId), ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new CreateLectureCompletionResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
