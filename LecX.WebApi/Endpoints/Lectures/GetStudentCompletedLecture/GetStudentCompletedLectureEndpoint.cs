using FastEndpoints;
using LecX.Application.Features.Lectures.GetStudentCompletedLecture;
using MediatR;

namespace LecX.WebApi.Endpoints.Lectures.GetStudentCompletedLecture
{
    public sealed class GetStudentCompletedLectureEndpoint(ISender sender) : Endpoint<GetStudentCompletedLectureRequest,GetStudentCompletedLectureResponse>
    {
        public override void Configure()
        {
            Get("/api/lectures/completed/students");
            Summary(s => s.Summary = "Get List Students Completed Lecture by Lecture ID");
        }
        public override async Task HandleAsync(GetStudentCompletedLectureRequest request, CancellationToken ct)
        {
            try
            {
                var response = await sender.Send(request, ct);
                await SendAsync(response, cancellation: ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new GetStudentCompletedLectureResponse { Message = ex.Message, Success = false }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
