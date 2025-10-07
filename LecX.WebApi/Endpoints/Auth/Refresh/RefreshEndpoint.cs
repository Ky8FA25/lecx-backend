using FastEndpoints;
using LecX.Application.Features.Auth.Refresh;
using LecX.WebApi.Endpoints.Auth.Common;
using MediatR;
using IMapper = AutoMapper.IMapper;

namespace LecX.WebApi.Endpoints.Auth.Refresh;

public sealed class RefreshEndpoint : EndpointWithoutRequest<RefreshResponse>
{
    private readonly ISender _sender;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;

    public RefreshEndpoint(ISender sender, IWebHostEnvironment env, IMapper mapper)
    {
        _sender = sender;
        _env = env;
        _mapper = mapper;
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

            var returnUrl = HttpContext.Request.Query["returnUrl"].FirstOrDefault() ?? "/";

            var command = new RefreshCommand
            {
                RefreshTokenPlain = refreshPlain,
                RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                ReturnUrl = returnUrl
            };

            var result = await _sender.Send(command, ct);

            // Set lại cookie với token mới (HttpOnly)
            AuthHelper.AppendRefreshCookie(HttpContext, result.RefreshTokenPlain, result.RefreshTokenExpiresUtc, _env.IsDevelopment());

            var res = _mapper.Map<RefreshResponse>(result);

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
}
