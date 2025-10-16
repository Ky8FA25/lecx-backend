using MediatR;
using FastEndpoints;
using LecX.Application.Features.Assignments.UpdateAssignment;

namespace LecX.WebApi.Endpoints.Assignments.UpdateAssignment
{
    public class UpdateAssignmentEndpoint (ISender sender) : Endpoint<UpdateAssignmentRequest, UpdateAssignmentResponse>
    {
        public override void Configure()
        {
            Put("/api/assignments/{assignmentId}");
            Summary(s =>
            {
                s.Summary = "Update an assignment by ID";
            });
            Roles("Instructor");
        }
        public override async Task HandleAsync(UpdateAssignmentRequest req, CancellationToken ct)
        {

            var response = await sender.Send(req, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
    
    }

