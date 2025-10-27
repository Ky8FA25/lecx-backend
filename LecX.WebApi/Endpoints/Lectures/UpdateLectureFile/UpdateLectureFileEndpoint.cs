using FastEndpoints;
using LecX.Application.Features.Lectures.UpdateLectureFile;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.UpdateLectureFile
{
    public class UpdateLectureFileEndpoint(ISender sender) : Endpoint<UpdateLectureFileRequest,UpdateLectureFileResponse>
    {
        public override void Configure()
        {
            Patch("/api/lectures/file");
            Summary(s => s.Summary = "Update a lecture file for a lecture");
            Roles("Admin", "Instructor");
        }
        public override async Task HandleAsync(UpdateLectureFileRequest req, CancellationToken ct)
            => await SendAsync(await sender.Send(req, ct), cancellation: ct);
    }
}
