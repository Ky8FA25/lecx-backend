using FastEndpoints;
using LecX.Application.Features.Courses.GetCourseById;
using MediatR;

namespace LecX.WebApi.Endpoints.Courses.GetCourseById
{
    public class GetCourseByIdEndpoint(ISender sender)
        : Endpoint<GetCourseByIdRequest, GetCourseByIdResponse>
    {
        public override void Configure()
        {
            Get("/api/courses/{CourseId}");
            AllowAnonymous();
            Summary(s => s.Summary = "Get course details by ID");
            Description(d => d.WithTags("Courses"));
        }

        public override async Task HandleAsync(GetCourseByIdRequest rq, CancellationToken ct) 
        {
            var result = await sender.Send(rq, ct);

            if (result.CourseDtos is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendOkAsync(result, ct);
        }
    }
}
