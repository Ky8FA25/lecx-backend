using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;
using NSwag;

namespace LecX.Infrastructure.Extensions.Swagger;

public static class SwaggerServiceExtension
{
    public static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services)
    {
        // FastEndpoints
        services.AddFastEndpoints();

        // FastEndpoints.Swagger
        services.SwaggerDocument(o =>
        {
            o.AutoTagPathSegmentIndex = 2;
            o.DocumentSettings = s =>
            {
                s.Title = "lecx-backend";
                s.Version = "v1"; // -> /swagger/v1/swagger.json
            };

            // (tuỳ version, có thể có o.EnableJWTBearerAuth = true; nhưng v5.24 dùng AddAuth như trên là chắc)
        });

        return services;
    }
}
