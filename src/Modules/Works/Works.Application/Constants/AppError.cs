namespace Works.Application.Constants;

internal static class AppError
{
    public static string GardeningWorkNotFound(int gardeningWorkId) => $"GardeningWork not found. [GardeningWorkId: {gardeningWorkId}]";

    public static string WorkItemNotFound(int workItemId) => $"WorkItem not found. [WorkItemId: {workItemId}]";
}