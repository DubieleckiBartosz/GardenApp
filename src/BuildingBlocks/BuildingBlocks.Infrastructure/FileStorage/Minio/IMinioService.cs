namespace BuildingBlocks.Infrastructure.FileStorage.Minio;

public interface IMinioService
{
    Task<byte[]?> GetFile(GetObjectArgs args, string objName);

    Task SaveFile(PutObjectArgs args);
}