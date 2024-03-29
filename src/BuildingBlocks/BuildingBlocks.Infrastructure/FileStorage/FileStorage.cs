namespace BuildingBlocks.Infrastructure.FileStorage;

public class FileStorage : IFileStorage
{
    private readonly IMinioService _minioService;

    public FileStorage(IMinioService minioService)
    {
        _minioService = minioService;
    }

    //Default max size equals 2MB
    public async Task<string?> Save(IFormFile formFile, string name, string bucket, int maxSize = 2097152)
    {
        await _minioService.CreateBucketWhenNotFound(bucket);

        using (var memoryStream = new MemoryStream())
        {
            await formFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            if (memoryStream.Length < maxSize)
            {
                //Create object args
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(name)
                    .WithStreamData(memoryStream)
                    .WithObjectSize(memoryStream.Length)
                    .WithContentType(formFile.ContentType);

                //Upload the file
                await _minioService.SaveFile(putObjectArgs);
            }
            else
            {
                return "The file is too large.";
            }
        }

        //Confirm upload
        //var statObj = new StatObjectArgs()
        //    .WithBucket(bucket)
        //    .WithObject(name);

        //await _minioService.ConfirmAsync(statObj);
        return null;
    }

    public async Task RemoveFileAsync(string name, string bucket)
    {
        var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(name);

        await _minioService.RemoveFileAsync(removeObjectArgs);
    }

    public async Task<byte[]?> GetFileAsync(string name, string bucket)
    {
        var getObjectArgs = new GetObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(name);

        return await _minioService.GetFile(getObjectArgs, name);
    }
}