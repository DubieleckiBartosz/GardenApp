namespace BuildingBlocks.Infrastructure.FileStorage.Minio;

public interface IMinioService
{
    Task<byte[]?> GetFile(GetObjectArgs args, string objName);

    Task CreateBucketWhenNotFound(string bucketName);

    Task SaveFile(PutObjectArgs args);

    Task RemoveFileAsync(RemoveObjectArgs args);

    Task ConfirmAsync(StatObjectArgs statObjectArgs);
}