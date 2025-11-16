using FastEndpoints;
using LecX.Application.Features.AssignmentScores.GetAssignmentScoreByCourseId;
using LecX.Application.Features.AssignmentScores.GetAssignmentScoreById;
using MediatR;

namespace LecX.WebApi.Endpoints.AssignmentScores.GetAssignmentScoreByCourseId
{
    public class GetAssignmentScoreByCourseIdEndpoint(ISender sender) : Endpoint<GetAssignmentScoreByCourseIdRequest, GetAssignmentScoreByCourseIdResponse>
    {
        public override void Configure()
        {
            Get("/api/assignmentscores/by-course/{courseId:int}");
            Summary(s =>
            {
                s.Summary = "Get all assignment score by CourseID";
                s.Description = "Retrieves an assignment score by its unique Id.";
                s.Response<GetAssignmentScoreByCourseIdResponse>(200, "Success");
            });
        }
        public override async Task HandleAsync(GetAssignmentScoreByCourseIdRequest rq, CancellationToken ct)
        {
            var result = await sender.Send(rq, ct);

            await SendAsync(result, cancellation: ct);

        }
    }
}
