using FastEndpoints;
using LecX.Application.Features.Lectures.CreateLecture;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.CreateLecture
{
    public class CreateLectureEndpoint(ISender sender) : Endpoint<CreateLectureRequest, CreateLectureResponse>
    {
        public override void Configure()
        {
            Post("/api/lectures");
            Summary(s => s.Summary = "Create a new lecture for a course");
            Roles("Admin", "Instructor");
        }
        public override async Task HandleAsync(CreateLectureRequest req, CancellationToken ct)
            => await SendAsync(await sender.Send(req, ct), cancellation: ct);
    }
}
