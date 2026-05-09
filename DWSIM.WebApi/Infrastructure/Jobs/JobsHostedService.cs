using ServiceStack;
using ServiceStack.Jobs;

namespace DWSIM.WebApi.Infrastructure.Jobs;

public sealed class JobsHostedService(ILogger<JobsHostedService> logger, IBackgroundJobs jobs) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("DWSIM database job runner started.");

        await jobs.StartAsync(stoppingToken).ConfigureAwait(false);

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(3));
        while (!stoppingToken.IsCancellationRequested &&
               await timer.WaitForNextTickAsync(stoppingToken).ConfigureAwait(false))
        {
            await jobs.TickAsync().ConfigureAwait(false);
        }
    }
}
