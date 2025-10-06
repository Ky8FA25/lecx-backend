using FastEndpoints;
using LecX.Application.Features.Courses.CreateCourse;
using LecX.Infrastructure.Extensions;

namespace WebApi
{
    public class Program
    {
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
            builder.Services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(p => p
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //fast endpoints & mediatR
            builder.Services.AddFastEndpoints();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateCourseHandler).Assembly);
            });

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

            app.UseForwardedHeaders();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseFastEndpoints();

            app.MapControllers();

            app.Run();
        }
    }
}
