using FastEndpoints;
using LecX.Application.Features.Auth.Common;
using LecX.Application.Features.Auth.Refresh;
using MediatR;

namespace LecX.WebApi.Endpoints.Auth;

public sealed class RefreshEndpoint : EndpointWithoutRequest<AuthResponse>
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public RefreshEndpoint(IMediator mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }

    public override void Configure()
    {
        Post("/api/auth/refresh");
        AllowAnonymous();
        Description(d => d.WithTags("Auth"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var refreshPlain = HttpContext.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshPlain))
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            var returnUrl = HttpContext.Request.Query["returnUrl"].FirstOrDefault();

            var req = new RefreshRequest
            {
                RefreshTokenPlain = refreshPlain,
                RequestIp = GetClientIp(HttpContext),
                ReturnUrl = returnUrl
            };

            var res = await _mediator.Send(req, ct);

            // Set lại cookie với token mới (HttpOnly)
            AppendRefreshCookie(HttpContext, res.RefreshTokenPlain, res.RefreshTokenExpiresUtc, _env.IsDevelopment());

            // Không trả refresh token trong body
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
            Logger?.LogError(ex, "Action failed");
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await HttpContext.Response.WriteAsJsonAsync(new { message = "Unexpected server error" }, cancellationToken: ct);
            return;
        }
    }

    private static string? GetClientIp(HttpContext ctx)
    {
        // nếu có proxy và đã UseForwardedHeaders, ưu tiên X-Forwarded-For
        if (ctx.Request.Headers.TryGetValue("X-Forwarded-For", out var v))
            return v.ToString().Split(',')[0].Trim();
        return ctx.Connection.RemoteIpAddress?.ToString();
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
