using ct.backend.Features.Auth.Common;
using FastEndpoints;
using FastEndpoints.Swagger;
using LecX.Infrastructure.Extensions;
using LecX.WebApi.Middleware;
using Microsoft.AspNetCore.HttpOverrides;

namespace WebApi
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });
            builder.Services.AddCoreInfrastructure(builder.Configuration);
            builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            builder.Services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(p => p
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.Configure<ForwardedHeadersOptions>(o =>
            {
                o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                o.ForwardLimit = 2;
                o.RequireHeaderSymmetry = false;
                //o.KnownNetworks.Clear();
                //o.KnownProxies.Clear();
            });

            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            var app = builder.Build();

            ////// Seed Database
            //using (var scope = app.Services.CreateScope())
            //{
            //    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //    var seeder = new DataSeeder(ctx, roleManager, userManager);
            //    await seeder.SeedAllAsync();
            //}
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseForwardedHeaders();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseFastEndpoints();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.UseSwaggerGen();

            app.MapControllers();

            app.Run();
        }
    }
}
