using MediatR;
using FastEndpoints;
using LecX.Application.Features.Submissions.CreateSubmission;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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
                req.UserId = userId!;
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

