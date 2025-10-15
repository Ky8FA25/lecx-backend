using FastEndpoints;
using LecX.Application.Features.Courses.DeleteCourse;
using MediatR;

namespace LecX.WebApi.Endpoints.Courses.DeleteCourse
{
    public sealed class DeleteCourseEndpoint(ISender sender) : Endpoint<DeleteCourseRequest, DeleteCourseResponse>
    {
        public override void Configure()
        {
            Delete("/api/courses/{CourseId:int}");
            Summary(s => s.Summary = "Delete a course by ID");
            Description(d => d.WithTags("Courses"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteCourseRequest rq, CancellationToken ct)
        {
            var result = await sender.Send(rq, ct);
            if (!result.IsDeleted)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendOkAsync(result, ct);
        }
    }
}
