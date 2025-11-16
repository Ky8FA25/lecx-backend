using LecX.Application.Features.Comments.CreateComment;
using LecX.Application.Features.InstructorConfirmations.CreateInstructorConfirmation;
using MediatR;
using System.Security.Claims;
using FastEndpoints;

namespace LecX.WebApi.Endpoints.InstructorConfirmations.CreateInstructorConfirmation
{
    public class CreateInstructorConfirmationEndpoint (ISender sender, IHttpContextAccessor httpContext): Endpoint<CreateInstructorConfirmationRequest, CreateInstructorConfirmationResponse>
    {
        public override void Configure()
        {
            Post("/api/instructor-confirmations");
            Summary(s =>
            {
                s.Summary = "Create new instructor confirmation";
            });
            Roles("Student");

        }
        public override async Task HandleAsync(CreateInstructorConfirmationRequest req, CancellationToken ct)
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
                    new(ex.Message), StatusCodes.Status500InternalServerError, ct);
            }
        }
    }
}
