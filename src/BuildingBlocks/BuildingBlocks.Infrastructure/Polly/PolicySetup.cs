namespace BuildingBlocks.Infrastructure.Polly;

public class PolicySetup
{
    public PolicySetup()
    {
    }

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