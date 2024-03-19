namespace Offers.Domain.Generators;

internal static class CodeGenerator
{
    private static Random random = new Random();

    internal static string GenerateUniqueCode()
    {
        var prefix = "GDN";
        var dateTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var randomNumbers = random.Next(1000, 9999).ToString();
        return $"{prefix}{dateTimeStamp}{randomNumbers}";
    }
}