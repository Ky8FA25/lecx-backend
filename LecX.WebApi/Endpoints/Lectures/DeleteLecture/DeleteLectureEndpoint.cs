using FastEndpoints;
using LecX.Application.Features.Lectures.DeleteLecture;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.DeleteLecture
{
    public class DeleteLectureEndpoint(ISender sender) : EndpointWithoutRequest<DeleteLectureResponse>
    {
        public override void Configure()
        {
            Delete("/api/lectures/{lectureId}");

            Summary(s =>
            {
                s.Summary = "Delete a lecture by ID";
            });
            Roles("Admin", "Instructor");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var lectureId = Route<int>("lectureId");
            var response = await sender.Send(new DeleteLectureRequest(lectureId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}