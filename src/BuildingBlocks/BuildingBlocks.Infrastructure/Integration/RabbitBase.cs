namespace BuildingBlocks.Infrastructure.Integration;

internal class RabbitBase : IRabbitBase
{
    private readonly Policy _policy;
    private const string DeadExchangeName = "dead_exchange";
    private const string DeadQueueName = "dead_queue";
    private const string DeadLetterRoutingKey = "dlx_key";

    private new readonly Dictionary<string, object> _args = new()
    {
        {"x-dead-letter-exchange", DeadExchangeName},
        {"x-dead-letter-routing-key", DeadLetterRoutingKey},
        {"x-message-ttl", 30000} ,
        {"x-max-length", 10000}
    };

    private readonly ILogger _logger;
    protected IModel? Channel { get; private set; }
    private IConnection? _connection;
    private readonly ConnectionFactory _connectionFactory;

    public RabbitBase(IOptions<RabbitOptions> options, ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        var rabbitOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));

        _connectionFactory = new ConnectionFactory
        {
            Port = rabbitOptions.Port,
            HostName = rabbitOptions.Host,
            Password = rabbitOptions.Password,
            UserName = rabbitOptions.User,
            DispatchConsumersAsync = true
        };

        var policySetup = new PolicySetup();
        _policy = policySetup.PolicyBrokerConnection(logger);
    }

    public IModel GetOrCreateNewModelWhenItIsClosed()
    {
        if (Channel is { IsOpen: true })
        {
            return Channel;
        }

        if (_connection == null || _connection.IsOpen == false)
        {
            _policy.Execute(() =>
            {
                _connection = _connectionFactory.CreateConnection();
            });
        }

        if (_connection is { IsOpen: true })
        {
            Channel = _connection.CreateModel();
            return Channel;
        }
        else
        {
            _logger.Fatal("FATAL ERROR: RabbitMQ connections could not be created and opened");
            throw new ArgumentNullException($"{Channel} cannot be null");
        }
    }

    public void CreateConsumer(IModel model, string exchangeName, string queueName, string routingKey,
        Dictionary<string, object> useArgs)
    {
        model.ExchangeDeclare(
            exchangeName,
            ExchangeType.Direct, true, false);

        model.QueueDeclare(
            queueName,
            true,
            false,
            false,
            useArgs);

        model.QueueBind(
            queueName,
            exchangeName,
            routingKey);
    }

    public void CreatePublisher(IModel model, string exchangeName, string routingKey, byte[] body)
    {
        model.ExchangeDeclare(
            exchangeName,
            ExchangeType.Direct,
            true,
            false);

        var properties = model.CreateBasicProperties();
        properties.MessageId = Guid.NewGuid().ToString("N");
        properties.CorrelationId = Guid.NewGuid().ToString("N");
        properties.ContentType = "application/json";

        _policy.Execute(() =>
        {
            model.BasicPublish(
                exchangeName, routingKey, body: body, basicProperties: properties);
        });
    }

    public async Task<Dictionary<string, object>> CreateDeadLetterQueue(IModel model)
    {
        model.ExchangeDeclare(
            DeadExchangeName,
            ExchangeType.Direct,
            true);

        model.QueueDeclare(
            DeadQueueName,
            true,
            false,
            false);

        model.QueueBind(DeadQueueName, DeadExchangeName, DeadLetterRoutingKey);

        var consumer = new AsyncEventingBasicConsumer(model);

        consumer.Received += (sender, eventArgs) =>
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var deathReasonBytes = (byte[])eventArgs.BasicProperties.Headers["x-first-death-reason"];

            var stringResult = Encoding.UTF8.GetString(deathReasonBytes);

            _logger.Information(new
            {
                DeadQueue = "------- DLX -------",
                UnprocessedMessage = message,
                Reason = stringResult
            }.Serialize());

            model.BasicReject(eventArgs.DeliveryTag, false);

            return Task.CompletedTask;
        };

        model.BasicConsume(queue: DeadQueueName, false, consumer: consumer);

        return await Task.FromResult(_args);
    }

    public void Dispose()
    {
        try
        {
            Channel?.Close();
            Channel?.Dispose();
            Channel = null;

            _connection?.Close();
            _connection?.Dispose();
            _connection = null;
        }
        catch (Exception ex)
        {
            _logger.Fatal(ex, "Cannot dispose RabbitMQ channel or connection");
        }
    }
}