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
        services
        .AddAuthentication()
        .AddGoogle(options =>
        {
            options.ClientId = config["Authentication:Google:ClientId"]
                ?? throw new InvalidOperationException("Missing Google:ClientId"); ;
            options.ClientSecret = config["Authentication:Google:ClientSecret"]
                ?? throw new InvalidOperationException("Missing Google:ClientSecret"); ;
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
        
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        return services;
    }
}
