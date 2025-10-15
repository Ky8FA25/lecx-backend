using FastEndpoints;
using LecX.Application.Features.Comments.UpdateComment;
using MediatR;

namespace LecX.WebApi.Endpoints.Comments.UpdateComment
{
    public class UpdateCommentEndpoint(ISender sender) : Endpoint<UpdateCommentRequest, UpdateCommentResponse>
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
            try
            {
                var res = await sender.Send(req, ct);
                await SendOkAsync(res, ct);
            }
            catch (KeyNotFoundException ex)
            {
                await SendAsync(
                    new() { Message = ex.Message }, StatusCodes.Status404NotFound, ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new() { Message = ex.Message }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
