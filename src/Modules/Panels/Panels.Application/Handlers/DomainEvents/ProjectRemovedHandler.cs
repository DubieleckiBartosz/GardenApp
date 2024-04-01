using Panels.Domain.Projects.Events;

namespace Panels.Application.Handlers.DomainEvents;

internal class ProjectRemovedHandler : IDomainEventHandler<ProjectRemoved>
{
    private readonly IFileStorage _fileStorage;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public ProjectRemovedHandler(IFileStorage fileStorage, IConfiguration configuration, ILogger logger)
    {
        _fileStorage = fileStorage;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Handle(DomainEvent<ProjectRemoved> notification, CancellationToken cancellationToken)
    {
        var @event = notification.Event;
        var logId = Guid.NewGuid().ToString();
        foreach (var image in @event.Images)
        {
            _logger.Warning($"Removing image {image} [LogId {logId}]");

            await _fileStorage.RemoveFileAsync(image, _configuration["FileCollections:ProjectImages"]!);
        }

        _logger.Warning($"Removed successfully [LogId {logId}]");
    }
}