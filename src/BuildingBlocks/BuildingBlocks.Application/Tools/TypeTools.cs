using BuildingBlocks.Application.Attributes;

namespace BuildingBlocks.Application.Tools;

public static class TypeTools
{
    public static string GetName(this string fullName)
    {
        var lastIndex = fullName.LastIndexOf('.');
        var typeName = lastIndex != -1 ? fullName.Substring(lastIndex + 1) : fullName;

        return typeName;
    }

    public static void RegistrationAssemblyIntegrationEvents(this Assembly assembly, IEventRegistry registry)
    {
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            var attribute = Attribute.GetCustomAttribute(type, typeof(IntegrationEventDecoratorAttribute));
            if (attribute == null)
            {
                continue;
            }

            var attr = (IntegrationEventDecoratorAttribute)attribute;
            var navigator = attr?.Navigator!;

            registry.Register(navigator, type);
        }
    }
}