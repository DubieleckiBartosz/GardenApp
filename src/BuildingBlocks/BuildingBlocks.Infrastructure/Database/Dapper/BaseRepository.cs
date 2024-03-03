namespace BuildingBlocks.Infrastructure.Database.Dapper;

public abstract class BaseRepository
{
    private readonly string _connectionString;
    private readonly ILogger _logger;
    private readonly AsyncPolicy _retryAsyncPolicyQuery;
    private readonly AsyncPolicy _retryAsyncPolicyConnection;

    protected BaseRepository(string dbConnection, ILogger logger)
    {
        _connectionString = dbConnection ??
                            throw new ArgumentNullException(nameof(dbConnection));

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        var policy = new PolicySetup();
        _retryAsyncPolicyConnection = policy.PolicyConnectionAsync(_logger);
        _retryAsyncPolicyQuery = policy.PolicyQueryAsync(_logger);
    }

    public async Task<T> WithConnection<T>(
        Func<IDbConnection, Task<T>> funcData,
        IDbTransaction? transaction = null,
        IDbConnection? dbConnection = null)
    {
        try
        {
            NpgsqlConnection? connection;

            if (transaction != null)
            {
                connection = transaction.Connection as NpgsqlConnection;
                return await _retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection!));
            }

            if (dbConnection != null)
            {
                return await _retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(dbConnection!));
            }

            await using (connection = new NpgsqlConnection(_connectionString))
            {
                await _retryAsyncPolicyConnection.ExecuteAsync(async () => await connection.OpenAsync());

                _logger.Information("Connection has been opened");

                return await _retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection));
            }
        }
        catch (TimeoutException ex)
        {
            throw new Exception($"Timeout npgsql exception: {ex}");
        }
        catch (NpgsqlException ex)
        {
            throw new Exception($"Npgsql exception: {ex}");
        }
    }

    protected async Task<IEnumerable<T>> QueryAsync<T>(string npgsql, object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.QueryAsync<T>(npgsql, param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string npgsql, Func<T1, T2, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.QueryAsync(npgsql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string npgsql,
        Func<T1, T2, T3, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.QueryAsync(npgsql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, T4, TReturn>(string npgsql,
        Func<T1, T2, T3, T4, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.QueryAsync(npgsql, map, splitOn: splitOn, param: param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }

    protected async Task<int> ExecuteAsync(string npgsql, object? param = null,
        CommandType? commandType = null, IDbConnection? dbConnection = null, IDbTransaction? transaction = null)
    {
        return await this.WithConnection(
            async _ => await _.ExecuteAsync(npgsql, param,
                commandType: commandType, transaction: transaction), transaction, dbConnection);
    }
}