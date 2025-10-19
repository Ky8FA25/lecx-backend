using FastEndpoints;
using LecX.Application.Features.Comments.UpdateComment;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Comments.UpdateComment
{
    public class UpdateCommentEndpoint(
        ISender sender,
        IHttpContextAccessor httpContext
        ) : Endpoint<UpdateCommentRequest, UpdateCommentResponse>
    {
        public override void Configure()
        {
            Put("/api/comments/{commentId:int}");
            Summary(s =>
            {
                s.Summary = "Edit comment";
            });
            Description(b => b
                .WithTags("Comments")
                .Produces<UpdateCommentResponse>());
        }
        public override async Task HandleAsync(UpdateCommentRequest req, CancellationToken ct)
        {
            var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            req.UserId = userId!;

            var res = await sender.Send(req, ct);
            await SendOkAsync(res, ct);
        }
    }
}
