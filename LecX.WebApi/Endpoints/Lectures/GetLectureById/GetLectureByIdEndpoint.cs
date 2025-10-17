using FastEndpoints;
using LecX.Application.Features.Lectures.GetLectureById;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.GetLectureById
{
    public class GetLecturesByIdEndpoint(ISender sender) : EndpointWithoutRequest<GetLectureByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/lectures/{lectureId}");
            Summary(s => s.Summary = "Get lecture detail by ID");
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var lectureId = Route<int>("lectureId");
            var response = await sender.Send(new GetLectureByIdRequest(lectureId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
}
