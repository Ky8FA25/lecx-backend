using FastEndpoints;
using LecX.Application.Features.Assignments.GetAssignmentsByCourse;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.WebApi.Endpoints.Assignments.GetAssignmentsByCourse
{
    public class GetAssignmentsByCourseEndpoint(ISender sender)
        : Endpoint<GetAssignmentsByCourseRequest,GetAssignmentsByCourseResponse>
    {
        public override void Configure()
        {
            Get("/api/assignments/filter");
            Summary(s =>
            {
                s.Summary = "Get filtered assignments by course ";
            });
            Roles("Admin", "Instructor", "Student");
        }
        public override async Task HandleAsync(GetAssignmentsByCourseRequest req , CancellationToken ct)
        {
            var request = new GetAssignmentsByCourseRequest(
                req.SearchWord,
                req.CourseId,
                req.DateSearch,
                req.PageIndex,
                req.PageSize
            );
            var response = await sender.Send(request, ct);
            await SendAsync(response, cancellation: ct);
        }
    }
    
    }

