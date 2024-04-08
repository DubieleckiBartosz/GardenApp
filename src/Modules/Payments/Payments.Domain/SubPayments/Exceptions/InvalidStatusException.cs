using BuildingBlocks.Domain.Exceptions;

namespace Payments.Domain.SubPayments.Exceptions;

internal class InvalidStatusException : BaseException
{
    internal InvalidStatusException(string customerSubscriptionId, int currentStatus, int newStatus)
        : base($"Status cannot be changed. [CustomerSubscriptionId: {customerSubscriptionId}, CurrentStatus: {currentStatus}. NewStatus: {newStatus}]")
    {
    }
}