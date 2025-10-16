using MediatR;
using LecX.Application.Features.Submissions.GetSubmissionsByAssignment;
using FastEndpoints;
namespace LecX.WebApi.Endpoints.Submissions.GetSubmissionsByAssignment
{
    public class GetSubmissionsByAssignmentEndpoint(ISender sender ) : Endpoint<GetSubmissionsByAssignmentRequest, GetSubmissionsByAssignmentResponse>
    {
        public override void Configure()
        {
            Get("api/submissions/assignment/{AssignmentId}");
            Summary(s => s.Summary = "Get Submission (paginated)");
            Description(x => x.WithTags("Submissions"));
            Roles("Instructor", "Student");
        }
        public override async Task HandleAsync(GetSubmissionsByAssignmentRequest req, CancellationToken ct)
        {
            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
    
    }

