using MediatR;
using FastEndpoints;
using LecX.Application.Features.Assignments.DeleteAssignment;
namespace LecX.WebApi.Endpoints.Assignments.DeleteAssignment
{
    public class DeleteAssignmentEndpoint(ISender sender)
        : EndpointWithoutRequest<DeleteAssignmentResponse>
    {
        public override void Configure()
        {
            Delete("/api/assignments/{assignmentId}");
            Summary(s =>
            {
                s.Summary = "Delete an assignment by ID";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var assignmentId = Route<int>("assignmentId");
            var response = await sender.Send(new DeleteAssignmentRequest(assignmentId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
    
    }

