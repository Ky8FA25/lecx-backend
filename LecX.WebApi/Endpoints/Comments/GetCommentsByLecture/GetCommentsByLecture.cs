using FastEndpoints;
using LecX.Application.Features.Comments.GetCommentById;
using LecX.Application.Features.Comments.GetCommentsByLecture;
using MediatR;

namespace LecX.WebApi.Endpoints.Comments.GetCommentsByLecture
{
    public class GetCommentsByLecture(ISender sender) : Endpoint<GetCommentsByLectureRequest, GetCommentsByLectureResponse>
    {
        public override void Configure()
        {
            Get("/api/comments");
            Summary(s =>
            {
                s.Summary = "Get comment by lecture";
                s.Description = "Retrieves a comment by lecture.";
                s.Response<GetCommentByIdResponse>(200, "Success");
            });
            Description(d => d.WithTags("Comments"));
        }
        public override async Task HandleAsync(GetCommentsByLectureRequest req, CancellationToken ct)
        {
            try
            {
                var response = await sender.Send(req, ct);
                await SendOkAsync(response, ct);
            }
            catch (KeyNotFoundException ex)
            {
                await SendAsync(
                    new() { Message = ex.Message }, StatusCodes.Status404NotFound, ct);
            }
        }
    }
}
