namespace BuildingBlocks.Application.Contracts;

public interface IFileStorage
{
    Task<string?> Save(IFormFile formFile, string bucket, int maxSize = 2097152);

    Task<byte[]?> GetFileAsync(string name, string bucket);
}