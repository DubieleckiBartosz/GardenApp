namespace BuildingBlocks.Application.Tools;

public static class StringTools
{
    public static string ReplaceWithDictionary(this string value, Dictionary<string, string> newData)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        foreach (var item in newData.Keys)
        {
            value = value.Replace("{" + item + "}", newData[item]);
        }

        return value;
    }
}