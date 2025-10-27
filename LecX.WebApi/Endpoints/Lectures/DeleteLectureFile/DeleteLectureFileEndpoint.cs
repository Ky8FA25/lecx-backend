using FastEndpoints;
using LecX.Application.Features.Lectures.DeleteLectureFile;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.DeleteLectureFile
{
    public class DeleteLectureFileEndpoint(ISender sender) : EndpointWithoutRequest<DeleteLectureFileResponse>
    {
        public override void Configure()
        {
            Delete("/api/lectures/file/{fileId}");
            Summary(s =>
            {
                s.Summary = "Delete a lecture file by ID";
            });
            Roles("Admin", "Instructor");
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var fileId = Route<int>("fileId");
            var response = await sender.Send(new DeleteLectureFileRequest(fileId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
