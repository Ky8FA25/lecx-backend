using MediatR;
using FastEndpoints;
using System.Threading;
using LecX.Application.Features.AssignmentScores.GetAssignmentScoreByAssignmentAndStudent;

namespace LecX.WebApi.Endpoints.AssignmentScores.GetAssignmentScoreByAssignmentAndStudent
{
    public class GetAssignmentScoreByAssignmentAndStudentEndpoint (ISender sender) : Endpoint<GetAssignmentScoreByAssignmentAndStudentRequest,GetAssignmentScoreByAssignmentAndStudentResponse>
    {
        public override void Configure()
        {
            Get("/api/assignmentscores/assignment/{assignmentId:int}/student/{studentId}");
            Summary(s =>
            {
                s.Summary = "Get an assignment score by Assignment ID and Student ID";
                s.Description = "Retrieves an assignment score based on the provided Assignment ID and Student ID.";
                s.Response<GetAssignmentScoreByAssignmentAndStudentResponse>(200, "Success");
            });
           
        }
        public override async Task HandleAsync(GetAssignmentScoreByAssignmentAndStudentRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);
            await SendAsync(result, cancellation: ct);

        }


    }
}
