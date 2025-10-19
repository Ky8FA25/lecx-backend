using FastEndpoints;
using LecX.Application.Abstractions.InternalServices.Certificates;
using LecX.Application.Features.Courses.CreateCourse;
using LecX.Infrastructure.Extensions.Database;
using LecX.Infrastructure.Extensions.GoogleAuth;
using LecX.Infrastructure.Extensions.GoogleStorage;
using LecX.Infrastructure.Extensions.Jwt;
using LecX.Infrastructure.Extensions.Mail;
using LecX.Infrastructure.Extensions.PayOS;
using LecX.Infrastructure.Extensions.Swagger;
using LecX.Infrastructure.InternalServices.Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace LecX.Infrastructure.Extensions
{
    public static class CoreServiceRegistration
    {
        [Obsolete]
        public static IServiceCollection AddCoreInfrastructure(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddDatabase(config);
            services.AddJwtAuthentication(config);
            services.AddMailService(config);
            services.AddGoogleAuthService(config);
            services.AddGoogleStorage(config);
            services.AddPayOSService(config);
            services.AddSwaggerWithAuth();
            services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddFastEndpoints();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCourseHandler).Assembly));
            services.AddScopedServicesByConvention
            (
                 appAssembly: typeof(ICertificateIssuanceService).Assembly,
                 infraAssembly: typeof(CertificateIssuanceService).Assembly
            );
            return services;
        }

        private static IServiceCollection AddScopedServicesByConvention(
            this IServiceCollection services,
            Assembly appAssembly,
            Assembly infraAssembly,
            string[]? allowedSuffixes = null)
        {
            allowedSuffixes ??= new[] { "Service", "Repository", "Provider", "Generator", "Client", "Gateway" };

            var appTypes = appAssembly.GetTypes();
            var infraTypes = infraAssembly.GetTypes();

            // 1) Lấy tất cả interface hợp lệ trong Application
            var interfaces = appTypes
                .Where(t =>
                    t.IsInterface &&
                    t.Name.StartsWith("I", StringComparison.Ordinal) &&
                    allowedSuffixes.Any(suf => t.Name.EndsWith(suf, StringComparison.Ordinal)))
                .ToArray();

            foreach (var iface in interfaces)
            {
                // 2) Tìm implementation trong Infrastructure
                var impls = infraTypes
                    .Where(c =>
                        c.IsClass &&
                        !c.IsAbstract &&
                        iface.IsAssignableFrom(c))
                    .ToArray();

                if (impls.Length == 0)
                    continue;

                // Ưu tiên map theo tên: IFooService → FooService
                var expectedName = iface.Name.StartsWith("I", StringComparison.Ordinal)
                    ? iface.Name.Substring(1)
                    : iface.Name;

                var preferred = impls.FirstOrDefault(c => string.Equals(c.Name, expectedName, StringComparison.Ordinal))
                               ?? impls.First();

                // 3) Đăng ký
                if (iface.IsGenericTypeDefinition && preferred.IsGenericTypeDefinition)
                {
                    // Open generic: IRepo<> -> Repo<>
                    services.TryAdd(new ServiceDescriptor(iface, preferred, ServiceLifetime.Scoped));
                }
                else
                {
                    services.TryAddScoped(iface, preferred);
                }
            }

            return services;
        }
    }
}
