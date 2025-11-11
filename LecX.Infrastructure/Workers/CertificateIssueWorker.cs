using LecX.Application.Abstractions.InternalServices.Certificates;
using LecX.Application.Abstractions.InternalServices.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LecX.Infrastructure.Workers
{
    public sealed class CertificateIssueWorker(
        IServiceProvider sp,
        IStudentCourseCompletionQueue queue,
        ILogger<CertificateIssueWorker> logger,
        IConfiguration config
    ) : BackgroundService
    {
        private readonly int _maxConcurrency = config.GetValue<int>("Worker:MaxConcurrency", 1);

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            logger.LogInformation("Worker START");
            var reader = queue.Reader;
            using var sem = new SemaphoreSlim(Math.Max(1, _maxConcurrency));

            await foreach (var key in reader.ReadAllAsync(ct))
            {
                await sem.WaitAsync(ct);
                _ = Task.Run(async () =>
                {
                    try
                    {
                        logger.LogInformation("Worker handling {StudentId}-{CourseId}", key.studentId, key.courseId);
                        using var scope = sp.CreateScope();
                        var issuer = scope.ServiceProvider.GetRequiredService<ICertificateIssuanceService>();
                        await issuer.IssueAsync(key.studentId, key.courseId, ct);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "IssueAsync failed for {StudentId}-{CourseId}", key.studentId, key.courseId);
                    }
                    finally
                    {
                        sem.Release();
                    }
                }, CancellationToken.None);
            }
        }
    }
}
