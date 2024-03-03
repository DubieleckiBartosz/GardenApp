namespace BuildingBlocks.Infrastructure.Polly;

public class PolicySetup
{
    private const int RetryCount = 3;

    public PolicySetup()
    {
    }

    public Policy PolicyBrokerConnection(ILogger logger) => Policy.Handle<BrokerUnreachableException>()
    .Or<SocketException>()
    .WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
    {
        logger.Warning(new
        {
            Message = "Could not publish event",
            Timeout = $"{time.TotalSeconds:n1}",
            ExceptionMessage = ex.Message
        }.Serialize());
    });

    public AsyncPolicy PolicyConnectionAsync(ILogger logger) => Policy
        .Handle<NpgsqlException>()
        .Or<TimeoutException>()
        .WaitAndRetryAsync(
            3,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (exception, timeSpan, retryCount, context) =>
            {
                logger?.Error(new
                {
                    RetryAttempt = retryCount,
                    ExceptionMessage = exception?.Message,
                    Waiting = timeSpan.Seconds
                }.Serialize());
            }
        );

    public AsyncPolicy PolicyQueryAsync(ILogger logger) => Policy.Handle<NpgsqlException>()
        .WaitAndRetryAsync(
            3,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (exception, timeSpan, retryCount, context) =>
            {
                logger?.Error(new
                {
                    RetryAttempt = retryCount,
                    ExceptionMessage = exception?.Message,
                    StackTrace = exception?.StackTrace,
                    Waiting = timeSpan.Seconds
                }.Serialize());
            }
        );
}