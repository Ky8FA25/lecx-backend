using LecX.Application.Abstractions.InternalServices.Certificates;
using LecX.Application.Abstractions.InternalServices.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace LecX.Infrastructure.Workers
{
    public sealed class CertificateIssueWorker : BackgroundService
    {
        private readonly IServiceProvider _sp;
        private readonly ChannelReader<(string studentId, int courseId)> _reader;
        private readonly ILogger<CertificateIssueWorker> _log;
        private readonly int _maxConcurrency;

        public CertificateIssueWorker(
            IServiceProvider sp,
            IStudentCourseCompletionQueue queue,
            ILogger<CertificateIssueWorker> log,
            IConfiguration cfg)
        {
            _sp = sp;
            _reader = queue.Reader;
            _log = log;
            _maxConcurrency = cfg.GetValue("Worker:MaxConcurrency", 2);
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            _log.LogInformation("Certificate Worker started with concurrency {C}", _maxConcurrency);

            using var sem = new SemaphoreSlim(_maxConcurrency);

            await foreach (var job in _reader.ReadAllAsync(ct))
            {
                await sem.WaitAsync(ct);

                _ = ProcessJobAsync(job, sem, ct);
            }
        }

        private async Task ProcessJobAsync(
            (string studentId, int courseId) job,
            SemaphoreSlim sem,
            CancellationToken ct)
        {
            try
            {
                int retry = 0;
                const int maxRetry = 3;

                while (true)
                {
                    try
                    {
                        using var scope = _sp.CreateScope();
                        var issuer = scope.ServiceProvider.GetRequiredService<ICertificateIssuanceService>();

                        await issuer.IssueAsync(job.studentId, job.courseId, ct);

                        _log.LogInformation("Issue done: {s}-{c}", job.studentId, job.courseId);
                        return;
                    }
                    catch (Exception ex)
                    {
                        retry++;

                        if (retry > maxRetry)
                        {
                            _log.LogError(ex, "FAILED after retry: {s}-{c}", job.studentId, job.courseId);
                            return; // ❌ Không retry nữa
                        }

                        int delay = retry * 1000;
                        _log.LogWarning("Retry {R}/{Max} for {s}-{c} after {D}ms", retry, maxRetry, job.studentId, job.courseId, delay);

                        await Task.Delay(delay, ct);
                    }
                }
            }
            finally
            {
                sem.Release();
            }
        }
    }

}
