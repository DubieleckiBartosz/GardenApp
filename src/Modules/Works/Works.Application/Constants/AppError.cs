namespace Works.Application.Constants;

internal static class AppError
{
    public static string WorkItemNotFound(int workItemId) => $"WorkItem not found. [WorkItemId: {workItemId}]";
}