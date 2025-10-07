using FastEndpoints;
using LecX.Application.Features.Auth.ResetPassword;
using MediatR;

namespace LecX.WebApi.Endpoints.Auth.ResetPassword
{
    public sealed class ResetPasswordEndpoint(
        AutoMapper.IMapper _mapper,
        ISender sender)
      : Endpoint<ResetPasswordRequest>
    {
        public const string Route = "/api/auth/reset-password";

        public override void Configure()
        {
            Post(Route);
            AllowAnonymous();
            Description(d => d.WithTags("Auth"));
        }
        public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
        {
            try
            {
                var command = _mapper.Map<ResetPasswordCommand>(req);
                await sender.Send(command, ct);
                await SendOkAsync(new { message = "Your password has been reset successfully." }, ct);
            }
            catch (InvalidOperationException ex)
            {
                await SendAsync(new { message = ex.Message }, StatusCodes.Status400BadRequest, ct);
                return;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Reset password failed");
                await SendAsync(new { message = "Unexpected server error" }, StatusCodes.Status500InternalServerError, ct);
                return;
            }
        }
    }
}
