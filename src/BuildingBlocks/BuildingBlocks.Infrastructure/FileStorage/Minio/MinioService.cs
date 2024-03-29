namespace BuildingBlocks.Infrastructure.FileStorage.Minio;

internal class MinioService : IMinioService
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger _logger;
    private readonly bool _isActive;

    public MinioService(IMinioFactory factory, IConfiguration configuration, ILogger logger)
    {
        _minioClient = factory.CreateClient();
        _logger = logger;
        _isActive = configuration.GetSection("MinioOptions:IsActive").Get<bool>();
    }

    public async Task<byte[]?> GetFile(GetObjectArgs args, string objName)
    {
        if (_isActive)
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

    public async Task CreateBucketWhenNotFound(string bucketName)
    {
        bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        if (!found)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }
    }

    public async Task SaveFile(PutObjectArgs args)
    {
        if (_isActive)
        {
            try
            {
                var response = await _minioClient.PutObjectAsync(args).ConfigureAwait(false);
                _logger.Information($"Minio upload response: {JsonConvert.SerializeObject(response)}");
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message, ex);
            }
        }
    }

    public async Task RemoveFileAsync(RemoveObjectArgs args)
    {
        if (_isActive)
        {
            try
            {
                await _minioClient.RemoveObjectAsync(args).ConfigureAwait(false);
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