namespace BuildingBlocks.Infrastructure.Integration.Interfaces;

internal interface IRabbitBase : IDisposable
{
    IModel GetOrCreateNewModelWhenItIsClosed();

    Task<Dictionary<string, object>> CreateDeadLetterQueue(IModel model);

    void CreatePublisher(IModel model, string exchangeName, string routingKey, byte[] body);

    void CreateConsumer(IModel model, string exchangeName, string queueName, string routingKey, Dictionary<string, object> useArgs);
}