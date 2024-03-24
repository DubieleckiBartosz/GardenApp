namespace Panels.Domain.Contractors.Exceptions;

internal class LogoNotFoundException : BaseException
{
    internal LogoNotFoundException(int contractorId) : base($"Logo not found. [ContractorId: {contractorId}]")
    {
    }
}