﻿namespace Panels.Infrastructure.Processing;

internal class InboxProcess : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IEventRegistry _eventRegistry;

    public InboxProcess(
        ILogger logger,
        IConfiguration configuration,
        IServiceScopeFactory serviceScopeFactory,
        IEventRegistry eventRegistry)
    {
        _logger = logger;
        _configuration = configuration;
        _serviceScopeFactory = serviceScopeFactory;
        _eventRegistry = eventRegistry;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Process(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(6), stoppingToken);
        }
    }

    private async Task Process(CancellationToken stoppingToken)
    {
        return;
        using var scope = _serviceScopeFactory.CreateScope();
        var accessor = scope.ServiceProvider.GetRequiredService<IInboxAccessor>();
        var messageIds = await accessor.GetUnprocessedMessageIdsAsync();

        var publishedMessages = new List<InboxMessage>();
        try
        {
            foreach (var messageId in messageIds)
            {
                _logger.Information("---------- Inbox - new process started ----------");

                _logger.Information($"---------- Inbox - process message id: {messageId} ----------");

                var message = await accessor.GetMessageAsync(messageId);
                if (message == null || message.ProcessedDate.HasValue)
                {
                    var isProcessed = message?.ProcessedDate != null ? true : false;
                    _logger.Warning($"---------- Inbox - stop process !!! [MessageId {messageId}]----------");
                    continue;
                }

                try
                {
                    var type = _eventRegistry.Navigate(message.Type.GetName());
                    var @event = JsonConvert.DeserializeObject(message.Data, type) as IntegrationEvent;
                    var dispatcher = scope.ServiceProvider.GetRequiredService<IEventDispatcher>();

                    await dispatcher.PublishAsync(stoppingToken, @event!);
                }
                catch (Exception ex)
                {
                    _logger.Error(new
                    {
                        Message = "Panels - Inbox process",
                        ErrorMessage = ex.Message,
                        StackTrace = ex.StackTrace
                    }.Serialize());
                    return;
                }

                await accessor.SetMessageToProcessedAsync(message.Id);

                _logger.Information($"---------- Inbox - message processed: {messageId} ----------");

                publishedMessages.Add(message);
            }

            if (publishedMessages.Any())
            {
                _logger.Information(
                    $"---------- Inbox - message to remove: {string.Join(", ", publishedMessages.Select(_ => _.Id))} ----------");
            }
        }
        finally
        {
            var deleteAfter = _configuration.GetSection("InboxOptions:DeleteAfter").Get<bool>();
            if (deleteAfter)
            {
                await accessor.DeleteAsync(publishedMessages);
            }
        }
    }
}