using FastEndpoints;
using LecX.Application.Features.Comments.DeleteComment;
using MediatR;

namespace LecX.WebApi.Endpoints.Comments.Delete
{
    public class DeleteCommentEndpoint(ISender sender)
        : Endpoint<DeleteCommentRequest, DeleteCommentResponse>
    {
        public override void Configure()
        {
            Delete("/api/comments/{commentId:int}");
            Summary(s =>
            {
                s.Summary = "Delete a comment by ID";
                s.Description = "Deletes a specific comment by its unique ID.";
                s.Response(200, "Deleted successfully");
                s.Response(404, "Comment not found");
                s.Response(400, "Delete failed");
            });
            Description(d => d.WithTags("Comments"));
        }

        public override async Task HandleAsync(DeleteCommentRequest req, CancellationToken ct)
        {
            try
            {
                var result = await sender.Send(req, ct);
                await SendAsync(
                    result, result.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, ct);
            }
            catch (KeyNotFoundException ex)
            {
                await SendAsync(
                    new () { Message = ex.Message}, StatusCodes.Status404NotFound, ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new () { Message = ex.Message }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
