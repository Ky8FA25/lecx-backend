using FastEndpoints;
using LecX.Application.Features.Lectures.CreateLectureCompletion;
using LecX.Application.Features.Lectures.DeleteLectureCompletion;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Lectures.DeleteLectureCompletion
{
    public class DeleteLectureCompletionEndpoint(ISender sender, IHttpContextAccessor httpContext) : EndpointWithoutRequest<DeleteLectureCompletionResponse>
    {
        public override void Configure()
        {
            Delete("/api/lectures/completed/{lectureId}");
            Summary(s => s.Summary = "Delete a record for completing lecture of a course (delete by student) ");
            Description(b => b
                .Produces<DeleteLectureCompletionResponse>(201)
                .Produces(400)
                .Produces(500)
            );
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                await SendAsync(
                    new DeleteLectureCompletionResponse { Message = "UserId not found", Success = false }, StatusCodes.Status400BadRequest, ct);
                return;
            }
            var lectureId = Route<int>("lectureId");
            var request = new DeleteLectureCompletionRequest(lectureId,userId);
            var response = await sender.Send(request, ct);
            if (response.Success)
                await SendAsync(response, 200, ct);
            else
                await SendAsync(response, 400, ct);
        }
    }
}
