using FastEndpoints;
using LecX.Application.Features.StudentCourses.GetCoursesFilteredByStudent;
using LecX.Application.Features.Submissions.CreateSubmission;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LecX.WebApi.Endpoints.Submissions.CreateSubmission
{
    public class CreateSubmissionEndpoint(ISender sender , IHttpContextAccessor httpContext) : Endpoint<CreateSubmissionRequest, CreateSubmissionResponse>
    {
        public override void Configure()
        {
            Post("/api/submissions");
            Summary(s =>
            {
                s.Summary = "Create new submission";
            });
            Description(b => b
                .WithTags("Submissions")
                .Produces<CreateSubmissionResponse>());

            Roles("Student", "Instructor");
        }
        public override async Task HandleAsync(CreateSubmissionRequest req, CancellationToken ct)
        {
            try
            {
                var userId = httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    await SendAsync(
                        new CreateSubmissionResponse { Message = "UserId not found", Success = false }, StatusCodes.Status400BadRequest, ct);
                    return;
                }
                req.StudentId = userId!;
                var res = await sender.Send(req, ct);
                await SendOkAsync(res, ct);
            }
            catch (Exception ex)
            {
                await SendAsync(
                    new() { Message = ex.Message }, StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
    
    }

