using LecX.Application.Abstractions.ExternalServices.Mail;
using LecX.Infrastructure.ExternalServices.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LecX.Infrastructure.Extensions.Mail;

public static class MailServiceRegistration
{
    public static IServiceCollection AddMailService(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        var mailConfigs = config.GetSection("MailSettings");
        if (mailConfigs == null)
        {
            throw new InvalidOperationException("MailSettings configuration section not found.");
        }

        services.Configure<MailSettings>(mailConfigs);
        services.AddTransient<IMailService, MailService>();

        return services;
    }
}
