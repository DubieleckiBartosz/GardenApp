namespace BuildingBlocks.Infrastructure.FileStorage.Minio;

public class MinioOptions
{
    public bool IsActive { get; set; }
    public string Endpoint { get; set; } = default!;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}