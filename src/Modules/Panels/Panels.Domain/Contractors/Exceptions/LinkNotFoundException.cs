namespace Panels.Domain.Contractors.Exceptions;

internal class LinkNotFoundException : BaseException
{
    public LinkNotFoundException(LinkType linkType, int contractorId) : base($"{linkType} link not found [ContractorId {contractorId}]")
    {
    }
}