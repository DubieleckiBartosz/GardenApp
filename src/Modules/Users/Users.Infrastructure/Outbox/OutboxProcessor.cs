namespace Users.Infrastructure.Outbox;

internal class OutboxProcessor : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}