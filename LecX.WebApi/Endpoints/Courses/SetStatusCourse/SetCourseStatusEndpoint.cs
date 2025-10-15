using FastEndpoints;
using LecX.Application.Features.Courses.SetStatusCourse;
using MediatR;

namespace LecX.WebApi.Endpoints.Courses.SetStatusCourse
{
    public sealed class SetCourseStatusEndpoint(ISender sender)
       : Endpoint<SetCourseStatusRequest, SetCourseStatusResponse>
    {
        public override void Configure()
        {
            Put("/api/courses/{courseId:int}/status");
            Summary(s => s.Summary = "Set course status (Admin/Instructor only)");
            Description(d => d.WithTags("Courses"));            
            Roles("Admin", "Instructor");
            
        }

        public override async Task HandleAsync(SetCourseStatusRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);
            if (!result.IsUpdated)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendOkAsync(result, ct);
        }
    }
}
