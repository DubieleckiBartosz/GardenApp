namespace BuildingBlocks.Infrastructure.FileStorage.Minio;

internal interface IMinioFactory
{
    IMinioClient CreateClient();
}

internal class MinioFactory : IMinioFactory
{
    private readonly IConfiguration _configuration;

    public MinioFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IMinioClient CreateClient()
    {
        var minioOptions = new MinioOptions();
        _configuration.GetSection(nameof(MinioOptions)).Bind(minioOptions);

        var client = new MinioClient()
            .WithEndpoint(minioOptions.Endpoint)
            .WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey) as MinioClient;

        return client.Build();
    }
}