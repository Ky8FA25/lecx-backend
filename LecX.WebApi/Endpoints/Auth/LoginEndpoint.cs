using FastEndpoints;
using LecX.Application.Features.Auth.Common;
using LecX.Application.Features.Auth.Login;
using MediatR;

namespace LecX.WebApi.Endpoints.Auth
{
    public sealed class LoginEndpoint : Endpoint<LoginRequest, AuthResponse>
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public LoginEndpoint(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        public override void Configure()
        {
            Post("/api/auth/login");
            AllowAnonymous();
            Description(d => d.WithTags("Auth"));
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            try
            {
                req.RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString();

                var res = await _mediator.Send(req, ct);

                // success: set cookie rồi trả 200
                AppendRefreshCookie(HttpContext, res.RefreshTokenPlain, res.RefreshTokenExpiresUtc, _env.IsDevelopment());
                res.RefreshTokenPlain = string.Empty;

                await SendOkAsync(res, ct);
            }
            catch (UnauthorizedAccessException ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await HttpContext.Response.WriteAsJsonAsync(new { message = ex.Message }, cancellationToken: ct);
                return;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Login failed");
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await HttpContext.Response.WriteAsJsonAsync(new { message = "Unexpected server error" }, cancellationToken: ct);
                return;
            }
        }


        private static void AppendRefreshCookie(HttpContext ctx, string refreshPlain, DateTime refreshExpiresUtc, bool isDev)
        {
            ctx.Response.Cookies.Append("refreshToken", refreshPlain, new CookieOptions
            {
                HttpOnly = true,
                Secure = !isDev,
                SameSite = SameSiteMode.Strict,
                Expires = refreshExpiresUtc
            });
        }
    }
}
