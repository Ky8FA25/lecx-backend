using FastEndpoints;
using IMapper = AutoMapper.IMapper;
using LecX.Application.Features.Auth.Login;
using MediatR;
using LecX.WebApi.Endpoints.Auth.Common;

namespace LecX.WebApi.Endpoints.Auth.Login
{
    public sealed class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
    {
        private readonly ISender _sender;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public LoginEndpoint(ISender sender, IWebHostEnvironment env, IMapper mapper)
        {
            _sender = sender;
            _env = env;
            _mapper = mapper;
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
                req.ReturnUrl = HttpContext.Request.Query["returnUrl"].FirstOrDefault() ?? "/";

                var command = _mapper.Map<LoginCommand>(req);
                command.RequestIp = HttpContext.Connection.RemoteIpAddress?.ToString();

                var result = await _sender.Send(command, ct);

                // success: set cookie rồi trả 200
                AuthHelper.AppendRefreshCookie(HttpContext, result.RefreshTokenPlain, result.RefreshTokenExpiresUtc, _env.IsDevelopment());

                var res = _mapper.Map<LoginResponse>(result)!;
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
    }
}
