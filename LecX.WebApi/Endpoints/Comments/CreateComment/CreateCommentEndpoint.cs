using FastEndpoints;
using LecX.Application.Features.Comments.CreateComment;
using MediatR;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Comments.CreateComment
{
    public class CreateCommentEndpoint(
        ISender sender,
        IHttpContextAccessor httpContext
        ) : Endpoint<CreateCommentRequest, CreateCommentResponse>
    {
        public override void Configure()
        {
            Post("/api/comments");
            Summary(s =>
            {
                s.Summary = "Create new comment";
            });
            Description(b => b
                .WithTags("Comments")
                .Produces<CreateCommentResponse>());
        }
        public override async Task HandleAsync(CreateCommentRequest req, CancellationToken ct)
        {
            try
            {
                var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
                req.UserId = userId!;

                var res = await sender.Send(req, ct);
                await SendOkAsync(res, ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new () { Message = ex.Message}, StatusCodes.Status500InternalServerError,  ct);
            }
        }
    }
}
