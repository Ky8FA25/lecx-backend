using FastEndpoints;
using LecX.Application.Features.Lectures.CreateLectureFile;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.CreateLectureFile
{
    public class CreateLectureFileEndpoint(ISender sender) : Endpoint<CreateLectureFileRequest,CreateLectureFileResponse>
    {
        public override void Configure()
        {
            Post("/api/lectures/file");
            Summary(s => s.Summary = "Create a new lecture file for a lecture");
            Roles("Admin", "Instructor");
        }
        public override async Task HandleAsync(CreateLectureFileRequest req, CancellationToken ct)
            => await SendAsync(await sender.Send(req, ct), cancellation: ct);
    }
}
