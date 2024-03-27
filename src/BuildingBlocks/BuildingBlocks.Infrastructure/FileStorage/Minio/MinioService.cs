namespace BuildingBlocks.Infrastructure.FileStorage.Minio;

internal class MinioService : IMinioService
{
    private readonly IMinioClient _minioClient;
    private readonly IOptionsMonitor<MinioOptions> _optionsMonitor;
    private readonly ILogger _logger;

    public MinioService(IMinioClient client, IOptionsMonitor<MinioOptions> optionsMonitor, ILogger logger)
    {
        _minioClient = client;
        _optionsMonitor = optionsMonitor;
        _logger = logger;
    }

    public async Task<byte[]?> GetFile(GetObjectArgs args, string objName)
    {
        var options = _optionsMonitor.Get(string.Empty);
        if (options.IsActive)
        {
            try
            {
                using (MemoryStream str = new MemoryStream())
                {
                    args.WithCallbackStream(stream => stream.CopyTo(str));
                    await _minioClient.GetObjectAsync(args);
                    return ReadAllBytes(str);
                }
            }
            catch (Exception ex)
            {
                _logger.Information($"MinIO error {ex.Message} for {objName}");
                return new byte[0];
            }
        }
        else
        {
            return new byte[0];
        }
    }

    public async Task SaveFile(PutObjectArgs args)
    {
        var options = _optionsMonitor.Get(string.Empty);
        if (options.IsActive)
        {
            try
            {
                await _minioClient.PutObjectAsync(args).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message, ex);
            }
        }
    }

    private byte[]? ReadAllBytes(Stream str)
    {
        if (str is MemoryStream)
        {
            var memoryStream = str as MemoryStream;
            return memoryStream?.ToArray();
        }

        using (var memoryStream = new MemoryStream())
        {
            str.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}