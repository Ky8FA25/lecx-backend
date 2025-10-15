using LecX.Application.Abstractions.ExternalServices.GoogleAuth;
using LecX.Infrastructure.ExternalServices.GoogleAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LecX.Infrastructure.Extensions.GoogleAuth;
public static class GoogleAuthServiceRegistration
{
    public static IServiceCollection AddGoogleAuthService(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        var section = config.GetSection("Authentication:Google");
        var s = section.Get<GoogleAuthSettings>()
        ?? throw new InvalidOperationException("Authentication:Google section is missing or invalid.");

        services
        .AddAuthentication()
        .AddGoogle((options) =>
        {
            options.ClientId = s.ClientId;
            options.ClientSecret = s.ClientSecret;
            options.CallbackPath = "/signin-google";
            // Explicitly request the profile scope
            options.Scope.Add("profile");

            // Map the picture to a claim
            options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
            options.Events.OnRemoteFailure = context =>
            {
                context.Response.Redirect("/api/auth/google-callback?error=access_denied");
                context.HandleResponse(); // chặn exception mặc định
                return Task.CompletedTask;
            };
        });
        services.Configure<GoogleAuthSettings>(section);
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        return services;
    }
}
