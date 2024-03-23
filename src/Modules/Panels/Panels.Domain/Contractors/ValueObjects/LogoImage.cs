namespace Panels.Domain.Contractors.ValueObjects;

internal class LogoImage : ValueObject
{
    public int ContractorId { get; }
    public string Key { get; }

    private LogoImage(int contractorId, string key)
    {
        ContractorId = contractorId;
        Key = key;
    }

    public static LogoImage CreateProjectImage(int contractorId, string key) => new LogoImage(contractorId, key);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ContractorId;
        yield return Key;
    }
}