using FastEndpoints;
using LecX.Application.Features.Lectures.GetLecturesByCourse;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.GetLecturesByCourse
{
    public class GetLecturesByCourseEndpoint(ISender sender) : Endpoint<GetLectureByCourseRequest,GetLectureByCourseResponse>
    {
        public override void Configure()
        {
            Get("/api/lectures/course");
            Summary(s => s.Summary = "Get List Lectures by Course ID");
        }
        public override async Task HandleAsync(GetLectureByCourseRequest request, CancellationToken ct)
        {
            try
            {
                var response = await sender.Send(request, ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new GetLectureByCourseResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
