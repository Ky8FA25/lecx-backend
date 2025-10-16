using MediatR;
using FastEndpoints;
using LecX.Application.Features.Assignments.GetAssignmentById;
namespace LecX.WebApi.Endpoints.Assignments.GetAssignmentById
{
    public class GetAssignmentByIdEndpoint(ISender sender): EndpointWithoutRequest<GetAssignmentByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/assignments/{assignmentId}");
            Summary(s =>
            {
                s.Summary = "Get assignment by assignment id";
            });
            Roles("Admin", "Instructor", "Student");
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            var assignmentId = Route<int>("assignmentId");
            var response = await sender.Send(new GetAssignmentByIdRequest(assignmentId), ct);
            await SendAsync(response, cancellation: ct);
        }
    }
    
    }

