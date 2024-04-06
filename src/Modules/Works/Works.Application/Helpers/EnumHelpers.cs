using System.Reflection;
using System.Runtime.Serialization;

namespace Works.Application.Helpers;

internal static class EnumHelpers
{
    public static string? GetEnumAttributeValueByString<T>(string value) where T : Enum
    {
        return typeof(T)
            .GetTypeInfo()
            .DeclaredMembers
            .SingleOrDefault(x => x.Name?.ToLower() == value.ToLower())
            ?.GetCustomAttribute<EnumMemberAttribute>(false)
            ?.Value;
    }

    public static List<string> GetEnumValues<T>() where T : Enum
    {
        return Enum.GetNames(typeof(T)).ToList();
    }
}