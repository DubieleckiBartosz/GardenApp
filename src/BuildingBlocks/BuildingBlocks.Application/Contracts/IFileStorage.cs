namespace BuildingBlocks.Application.Contracts;

public interface IFileStorage
{
    Task<string?> Save(IFormFile formFile, string name, string bucket, int maxSize = 2097152);

    Task RemoveFileAsync(string name, string bucket);

    Task<byte[]?> GetFileAsync(string name, string bucket);
}