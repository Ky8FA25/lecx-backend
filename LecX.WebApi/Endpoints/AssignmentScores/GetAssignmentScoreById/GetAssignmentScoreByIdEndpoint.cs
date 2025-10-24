using FastEndpoints;
using LecX.Application.Features.AssignmentScores.Common;
using LecX.Application.Features.AssignmentScores.GetAssignmentScoreById;
using LecX.Application.Features.CourseMaterials.GetCourseMaterialById;
using LecX.Application.Features.Courses.GetCourseById;
using MediatR;
namespace LecX.WebApi.Endpoints.AssignmentScores.GetAssignmentScoreById
{
    public sealed class GetAssignmentScoreByIdEndpoint(ISender sender) : Endpoint<GetAssignmentScoreByIdRequest, GetAssignmentScoreByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/assignmentscores/{assignmentScoreId:int}");
            Summary(s =>
            {
                s.Summary = "Get an assignment score by ID";
                s.Description = "Retrieves an assignment score by its unique Id.";
                s.Response<GetAssignmentScoreByIdResponse>(200, "Success");
            });
        }
        public override async Task HandleAsync(GetAssignmentScoreByIdRequest rq, CancellationToken ct)
        {
            var result = await sender.Send(rq, ct);

            await SendAsync(result, cancellation: ct);


        }
    }
}
