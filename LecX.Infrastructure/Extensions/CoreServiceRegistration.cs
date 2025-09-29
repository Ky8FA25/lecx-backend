using LecX.Infrastructure.Extensions.Database;
using LecX.Infrastructure.Extensions.GoogleAuth;
using LecX.Infrastructure.Extensions.Jwt;
using LecX.Infrastructure.Extensions.Mail;
using LecX.Infrastructure.Extensions.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LecX.Infrastructure.Extensions
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreInfrastructure(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddDatabase(config);
            services.AddJwtAuthentication(config);
            services.AddMailService(config);
            services.AddGoogleAuthService(config);
            //services.AddStorageService(config);
            //services.AddRabbitMq(config);
            //services.AddPayment(config);
            //services.AddHuggingfaceService();
            services.AddSwaggerWithAuth();
            services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }
    }
}
