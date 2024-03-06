using Microsoft.Extensions.Hosting;

namespace Panels.Infrastructure.Processing;

internal class InboxProcess : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}