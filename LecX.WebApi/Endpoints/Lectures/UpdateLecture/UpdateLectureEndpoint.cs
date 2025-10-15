using FastEndpoints;
using LecX.Application.Features.Lectures.UpdateLecture;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.UpdateLecture
{
    public sealed class UpdateLectureEndpoint(ISender sender) : Endpoint<UpdateLectureRequest, UpdateLectureResponse>
    {
        public override void Configure()
        {
            Patch("/api/lectures");
            Summary(s => s.Summary = "Update a lecture for a course");
            Roles("Admin", "Instructor");
        }
        public override async Task HandleAsync(UpdateLectureRequest req, CancellationToken ct)
            => await SendAsync(await sender.Send(req, ct), cancellation: ct);
    }
}
