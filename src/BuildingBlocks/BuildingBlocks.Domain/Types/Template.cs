namespace BuildingBlocks.Domain.Types;

public class Template
{
    public short Id { get; }
    public int TemplateType { get; set; }
    public string Subject { get; } = default!;
    public string Value { get; } = default!;

    private Template()
    { }

    public Template(int templateType, string uniqueSubject, string value)
    {
        TemplateType = templateType;
        Subject = uniqueSubject;
        Value = value;
    }
}