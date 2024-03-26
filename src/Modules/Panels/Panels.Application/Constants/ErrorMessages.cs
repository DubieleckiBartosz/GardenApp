namespace Panels.Application.Constants;

internal static class ErrorMessages
{
    internal const string ContractorExists = "Contractor already exists";

    //Methods
    internal static string ContractorNotFound(string businessId) => $"Contractor not found. [BusinessId: {businessId}]";
}