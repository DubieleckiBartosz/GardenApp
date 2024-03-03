namespace BuildingBlocks.Application.Clients.Module;

public static class ModuleExtensions
{
    public static string ReadModuleName(this Type targetType)
    {
        var assembly = Assembly.GetAssembly(targetType);
        var assemblyName = assembly!.GetName();

        var parts = assemblyName.Name!.Split('.');
        var shortName = parts[0];

        return shortName;
    }
}