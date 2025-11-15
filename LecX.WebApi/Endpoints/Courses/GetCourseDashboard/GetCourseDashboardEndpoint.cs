using FastEndpoints;
using LecX.Application.Features.Courses.GetCourseDashboard;
using MediatR;

namespace LecX.WebApi.Endpoints.Courses.GetCourseDashboard
{
    public sealed class GetCourseDashboardEndpoint(ISender sender)
       : Endpoint<GetCourseDashboardRequest, GetCourseDashboardResponse>
    {
        public override void Configure()
        {
            Get("/api/instructor/courses/{courseId}/dashboard");
            Summary(s => s.Summary = "Get course dashboard data for instructor");
            Description(d => d.WithTags("Courses", "Dashboard"));
            Roles("Instructor", "Admin");
        }

        public override async Task HandleAsync(GetCourseDashboardRequest req, CancellationToken ct)
        {
            // Map route parameter to request
            var courseId = Route<int>("courseId");
            var request = new GetCourseDashboardRequest(courseId);
            
            var result = await sender.Send(request, ct);
            
            if (!result.Success)
            {
                await SendAsync(result, StatusCodes.Status404NotFound, ct);
                return;
            }
            
            await SendOkAsync(result, ct);
        }
    }
}

