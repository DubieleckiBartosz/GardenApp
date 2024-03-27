namespace BuildingBlocks.Infrastructure.FileStorage;

public class FileStorage : IFileStorage
{
    private readonly IMinioService _minioService;

    public FileStorage(IMinioService minioService)
    {
        _minioService = minioService;
    }

    //Default max size equals 2MB
    public async Task<string?> Save(IFormFile formFile, string bucket, int maxSize = 2097152)
    {
        using (var memoryStream = new MemoryStream())
        {
            await formFile.CopyToAsync(memoryStream);
            if (memoryStream.Length < maxSize)
            {
                var byteArray = memoryStream.ToArray();
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(formFile.Name)
                    .WithStreamData(memoryStream)
                    .WithObjectSize(byteArray.Length);

                await _minioService.SaveFile(putObjectArgs);
                return null;
            }
            else
            {
                return "The file is too large.";
            }
        }
    }

    public async Task<byte[]?> GetFileAsync(string name, string bucket)
    {
        var getObjectArgs = new GetObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(name);

        return await _minioService.GetFile(getObjectArgs, name);
    }
}