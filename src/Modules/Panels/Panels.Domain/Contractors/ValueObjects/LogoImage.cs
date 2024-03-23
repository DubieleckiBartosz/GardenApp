namespace Panels.Domain.Contractors.ValueObjects;

internal class LogoImage : ValueObject
{
    public Guid ContractorId { get; }
    public string Key { get; }

    private LogoImage(Guid contractorId, string key)
    {
        ContractorId = contractorId;
        Key = key;
    }

    public static LogoImage CreateProjectImage(Guid contractorId, string key) => new LogoImage(contractorId, key);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ContractorId;
        yield return Key;
    }
}