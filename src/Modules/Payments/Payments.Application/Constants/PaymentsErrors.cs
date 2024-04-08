namespace Payments.Application.Constants;

internal static class PaymentsErrors
{
    internal const string TemplateNotFound = "Template not found.";

    internal static string PayerNotFound(string userId) => $"Payer not found. [UserId: {userId}]";

    internal static string SubPaymentNotFound(string userId, int payerId) => $"Subscription payment not found. [UserId: {userId}, PayerId: {payerId}]";

    internal static string SessionNotFound(string sessionId) => $"Session not found. [SessionId: {sessionId}]";
}