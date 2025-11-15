using FastEndpoints;
using LecX.Application.Features.Comments.DeleteComment;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Comments.Delete
{
    public class DeleteCommentEndpoint(
        ISender sender,
        IHttpContextAccessor httpContext
        ) : Endpoint<DeleteCommentRequest, DeleteCommentResponse>
    {
        public override void Configure()
        {
            Delete("/api/comments/{commentId:int}");
            Summary(s =>
            {
                s.Summary = "Delete a comment by ID";
                s.Description = "Soft-delete a specific comment by its unique ID.";
                s.Response(200, "Deleted successfully");
                s.Response(403, "Forbidden");
                s.Response(404, "Comment not found");
                s.Response(400, "Delete failed");
            });
            Description(d => d.WithTags("Comments"));
        }

        public override async Task HandleAsync(DeleteCommentRequest req, CancellationToken ct)
        {
            var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            req.UserId = userId!;

            var result = await sender.Send(req, ct);

            if (result.Success)
            {
                await SendOkAsync(result, ct);
                return;
            }

            await SendAsync(result, StatusCodes.Status400BadRequest, ct);
        }
    }
}
