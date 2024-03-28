using BuildingBlocks.Domain.Time;

namespace Panels.Application.Tools;

internal static class FileNameCreator
{
    internal static string CreateName(this IFormFile formFile)
    {
        var fileExtension = Path.GetExtension(formFile.FileName);
        var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
        var dateSection = Clock.CurrentDate().ToString("yyyyMMddHHmmssfff");
        var uniqueFileName = $"{fileName}-{dateSection}{fileExtension}";

        return uniqueFileName;
    }
}