using FastEndpoints;
using LecX.Application.Features.Comments.GetCommentById;
using MediatR;

namespace LecX.WebApi.Endpoints.Comments.GetCommentById
{
    public class GetCommentByIdEndpoint(
        ISender sender
        ) : Endpoint<GetCommentByIdRequest, GetCommentByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/comments/{commentId:int}");
            Summary(s =>
            {
                s.Summary = "Get a comment by ID";
                s.Description = "Retrieves a comment by its unique identifier.";
                s.Response<GetCommentByIdResponse>(200, "Success");
            });
            Description(d => d.WithTags("Comments"));
        }
        public override async Task HandleAsync(GetCommentByIdRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendOkAsync(response, ct);
        }
    }
}
