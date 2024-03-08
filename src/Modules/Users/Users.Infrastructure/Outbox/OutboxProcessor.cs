using BuildingBlocks.Application.Contracts.Integration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Users.Infrastructure.Outbox;

internal class OutboxProcessor : BackgroundService
{
    private readonly IEventBus _eventBus;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IEventRegistry _eventRegistry;

    public OutboxProcessor(
        IEventBus eventBus,
        ILogger logger,
        IConfiguration configuration,
        IEventRegistry eventRegistry,
        IServiceScopeFactory serviceScopeFactory)
    {
        _eventBus = eventBus;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _configuration = configuration;
        _eventRegistry = eventRegistry;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Process();

            await Task.Delay(TimeSpan.FromSeconds(6), stoppingToken);
        }
    }

    private async Task Process()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var accessor = scope.ServiceProvider.GetRequiredService<IOutboxAccessor>();
        var messageIds = await accessor.GetUnprocessedMessageIdsAsync();

        var publishedMessages = new List<OutboxMessage>();
        try
        {
            foreach (var messageId in messageIds)
            {
                _logger.Information("---------- Outbox - New Process Started ----------");

                _logger.Information($"---------- Outbox - Process Message Id: {messageId} ----------");

                var message = await accessor.GetMessageAsync(messageId);
                if (message == null || message.ProcessedDate.HasValue)
                {
                    var isProcessed = message?.ProcessedDate != null ? true : false;
                    _logger.Warning($"---------- Outbox - Stop Process !!! [MessageId {messageId}]----------");
                    continue;
                }

                var destinationType = _eventRegistry.Navigate(Type.GetType(message.Type)!.Name);

                var @event = JsonConvert.DeserializeObject(message.Data, destinationType) as IntegrationEvent;
                await _eventBus.Publish(@event!);

                await accessor.SetMessageToProcessedAsync(message.Id);

                _logger.Information($"---------- Outbox - Message Processed: {messageId} ----------");

                publishedMessages.Add(message);
            }

            if (publishedMessages.Any())
            {
                _logger.Information(
                    $"---------- Outbox - Message to remove: {string.Join(", ", publishedMessages.Select(_ => _.Id))} ----------");
            }
        }
        finally
        {
            var deleteAfter = _configuration.GetSection("OutboxOptions:DeleteAfter").Get<bool>();
            if (deleteAfter)
            {
                await accessor.DeleteAsync(publishedMessages);
            }
        }
    }
}