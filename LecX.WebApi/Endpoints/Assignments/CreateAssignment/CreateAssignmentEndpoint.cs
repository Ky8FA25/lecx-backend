using MediatR;
using LecX.Application.Features.Assignments.CreateAssignment;
using FastEndpoints;

namespace LecX.WebApi.Endpoints.Assignments.CreateAssignment
{
    public class CreateAssignmentEndpoint(ISender sender):Endpoint<CreateAssignmentRequest, CreateAssignmentResponse>
    {
        public override void Configure()
        {
            Post("/api/assignments");
            Summary(s => s.Summary = "Create a new assignment for a course");
            Roles("Instructor");
        }
        public override async Task HandleAsync(CreateAssignmentRequest req, CancellationToken ct)
        {
            var res = await sender.Send(req, ct);
            await SendAsync(res, cancellation: ct);   
        }
    }
    
    }

