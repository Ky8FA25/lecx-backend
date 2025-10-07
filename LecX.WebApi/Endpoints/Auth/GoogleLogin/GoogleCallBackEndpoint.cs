using FastEndpoints;
using LecX.Application.Features.Auth.GoogleCallBack;
using LecX.Domain.Entities;
using LecX.WebApi.Endpoints.Auth.Common;
using LecX.WebApi.Endpoints.Auth.GoogleLogin;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public sealed class GoogleCallbackEndpoint(
    ISender sender,
    SignInManager<User> signIn,
    IWebHostEnvironment env
) : Endpoint<GoogleCallbackRequest>
{
    public const string Route = "/api/auth/google-callback"; // 👈 chỉ đổi ở đây
    public override void Configure()
    {
        Get(Route);
        AllowAnonymous();
        Description(d => d.WithTags("Auth"));
    }

    public override async Task HandleAsync(GoogleCallbackRequest req, CancellationToken ct)
    {
        var info = await signIn.GetExternalLoginInfoAsync();
        if (info is null)
        {
            await SendHtmlAndClose(@"window.opener?.postMessage({ error: 'Login Failed' }, '*');");
            return;
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var name = info.Principal.FindFirstValue(ClaimTypes.Name);
        var avatar = info.Principal.FindFirstValue("urn:google:picture");

        if (string.IsNullOrWhiteSpace(email))
        {
            await SendHtmlAndClose(@"window.opener?.postMessage({ error: 'No Email' }, '*');");
            return;
        }

        // tách tên
        var parts = (name ?? email).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var firstName = parts.FirstOrDefault() ?? email;
        var lastName = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";

        // call handler
        var result = await sender.Send(new GoogleCallbackCommand(
            Provider: info.LoginProvider,
            ProviderKey: info.ProviderKey,
            Email: email,
            FullName: name,
            FirstName: firstName,
            LastName: lastName,
            AvatarUrl: avatar,
            ClientIp: HttpContext.Connection.RemoteIpAddress?.ToString()
        ), ct);

        // set refresh cookie (HTTP concern)
        AuthHelper.AppendRefreshCookie(HttpContext, result.RefreshTokenPlain, result.RefreshTokenExpiresUtc, env.IsDevelopment());

        // postMessage & close popup
        var payloadJson = System.Text.Json.JsonSerializer.Serialize(new
        {
            token = result.Jwt,
            user = result.User,
            returnUrl = req.ReturnUrl
        });

        var script = $@"(function(){{
            var params = new URLSearchParams(window.location.search);
            var openerFromQuery = params.get('opener');
            var openerOrigin = openerFromQuery || (document.referrer ? new URL(document.referrer).origin : '*');
            var payload = {payloadJson};
            if (window.opener && !window.opener.closed) {{
                window.opener.postMessage(payload, openerOrigin);
            }}
            setTimeout(function(){{ window.close(); }}, 0);
        }})();";

        await SendHtmlAndClose(script);
    }

    private async Task SendHtmlAndClose(string inlineJs)
    {
        HttpContext.Response.ContentType = "text/html; charset=utf-8";
        await HttpContext.Response.WriteAsync($@"<!doctype html><html><body><script>{inlineJs}</script></body></html>");
    }
}