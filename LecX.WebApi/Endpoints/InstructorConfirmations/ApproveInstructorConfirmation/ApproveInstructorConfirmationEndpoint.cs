using FastEndpoints;
using LecX.Application.Features.InstructorConfirmations.ApproveInstructorConfirmation;
using MediatR;

namespace LecX.WebApi.Endpoints.InstructorConfirmations.ApproveInstructorConfirmation
{
    public sealed class ApproveInstructorConfirmationEndpoint(ISender sender)
        : Endpoint<ApproveInstructorConfirmationRequest, ApproveInstructorConfirmationResponse>
    {
        public override void Configure()
        {
            Put("/api/instructor-confirmations/approve");
            Summary(s =>
            {
                s.Summary = "Approve instructor confirmation and create Instructor record";
                s.Description = "Admin only endpoint to approve an instructor confirmation request";
            });
            Description(b => b
                .Produces<ApproveInstructorConfirmationResponse>());
            Roles("Admin");
        }

        public override async Task HandleAsync(ApproveInstructorConfirmationRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);

            if (!result.Success)
            {
                await SendAsync(result, StatusCodes.Status400BadRequest, ct);
                return;
            }

            await SendOkAsync(result, ct);
        }
    }
}
