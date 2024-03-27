namespace BuildingBlocks.Infrastructure.FileStorage.Minio;

//https://github.dev/appany/Minio.AspNetCore
public class MinioFactory : IMinioFactory
{
    private readonly IOptionsMonitor<MinioOptions> optionsMonitor;

    public MinioFactory(IOptionsMonitor<MinioOptions> optionsMonitor)
    {
        this.optionsMonitor = optionsMonitor;
    }

    public IMinioClient CreateClient()
    {
        return CreateClient(string.Empty);
    }

    public IMinioClient CreateClient(string name)
    {
        var options = optionsMonitor.Get(name);

        var client = new MinioClient()
          .WithEndpoint(options.Endpoint)
          .WithCredentials(options.AccessKey, options.SecretKey)
          .WithSessionToken(options.SessionToken) as MinioClient;

        if (!string.IsNullOrEmpty(options.Region))
        {
            client.WithRegion(options.Region);
        }

        options.Configure?.Invoke(client!);

        return client.Build();
    }
}