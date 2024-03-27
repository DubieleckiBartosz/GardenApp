namespace BuildingBlocks.Infrastructure.FileStorage.Minio;

public interface IMinioFactory
{
    IMinioClient CreateClient();

    IMinioClient CreateClient(string name);
}