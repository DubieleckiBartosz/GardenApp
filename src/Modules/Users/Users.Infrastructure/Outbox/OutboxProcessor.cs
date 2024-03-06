using BuildingBlocks.Application.Contracts.Integration;
using BuildingBlocks.Application.Settings;
using BuildingBlocks.Application.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace Users.Infrastructure.Outbox;

internal class OutboxProcessor : BackgroundService
{
    private readonly IEventBus _eventBus;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public OutboxProcessor(IEventBus eventBus, IServiceScopeFactory serviceScopeFactory, ILogger logger, IConfiguration configuration)
    {
        _eventBus = eventBus;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _configuration = configuration;
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
        var store = scope.ServiceProvider.GetRequiredService<IOutboxStore>();
        var messageIds = await store.GetUnprocessedMessageIdsAsync();

        var publishedMessages = new List<OutboxMessage>();
        try
        {
            foreach (var messageId in messageIds)
            {
                _logger.Information("---------- New Process Started ----------");

                _logger.Information($"---------- Process Message Id: {messageId} ----------");

                var message = await store.GetMessageAsync(messageId);
                if (message == null || message.ProcessedDate.HasValue)
                {
                    var isProcessed = message?.ProcessedDate != null ? true : false;
                    _logger.Warning($"---------- Stop Process !!! [MessageId {messageId}]----------");

                    continue;
                }

                var @event = JsonConvert.DeserializeObject<IntegrationEvent>(message.Type, JsonSettings.DefaultSerializerSettings);

                await _eventBus.Publish(@event!);
                await store.SetMessageToProcessedAsync(message.Id);

                _logger.Information($"---------- Message Processed: {messageId} ----------");

                publishedMessages.Add(message);
            }

            if (publishedMessages.Any())
            {
                _logger.Information(
                    $"---------- Message to remove: {string.Join(", ", publishedMessages.Select(_ => _.Id))} ----------");
            }
        }
        finally
        {
            var deleteAfter = _configuration.GetSection("OutboxOptions:DeleteAfter").Get<bool>();
            if (deleteAfter)
            {
                store.Delete(publishedMessages);
            }
        }
    }
}