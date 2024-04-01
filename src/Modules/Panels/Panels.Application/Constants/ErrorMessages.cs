namespace Panels.Application.Constants;

internal static class ErrorMessages
{
    internal const string ContractorExists = "Contractor already exists";

    //Methods
    internal static string ContractorNotFound(string businessId) => $"Contractor not found. [BusinessId: {businessId}]";

    internal static string ProjectNotFound(int projectId, string businessId) => $"Project not found. [ProjectId: {projectId}, BusinessId: {businessId}]";
}