using FastEndpoints;
using LecX.Application.Features.Lectures.GetLecturesCompletedByUser;
using MediatR;
using Org.BouncyCastle.Ocsp;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Lectures.GetLecturesCompletedByUser
{
    public class GetLecturesCompletedByUserEndpoint(ISender sender, IHttpContextAccessor httpContext) : Endpoint<GetLectureCompletedByUserRequest,GetLectureCompletedByUserResponse>
    {
        public override void Configure()
        {
            Get("/api/lectures/course/completed");
            Summary(s => s.Summary = "Get List Lectures Completed By User In Course");
        }
        public override async Task HandleAsync(GetLectureCompletedByUserRequest request,CancellationToken ct)
        {
            try
            {
                var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    await SendAsync(
                        new GetLectureCompletedByUserResponse { Message = "UserId not found", Success = false }, StatusCodes.Status400BadRequest, ct);
                    return;
                }
                request.UserId = userId!;
                var response = await sender.Send(request, ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new GetLectureCompletedByUserResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
