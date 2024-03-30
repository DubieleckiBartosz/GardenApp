namespace Panels.Application.Handlers.DomainEvents;

internal class LogoRemovedHandler : IDomainEventHandler<LogoRemoved>
{
    private readonly IFileStorage _fileStorage;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public LogoRemovedHandler(IFileStorage fileStorage, IConfiguration configuration, ILogger logger)
    {
        _fileStorage = fileStorage;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Handle(DomainEvent<LogoRemoved> notification, CancellationToken cancellationToken)
    {
        var @event = notification.Event;
        await _fileStorage.RemoveFileAsync(@event!.Logo!, _configuration["FileCollections:LogoImages"]!);

        _logger.Warning($"Removed successfully [logo key: {@event!.Logo}]");
    }
}