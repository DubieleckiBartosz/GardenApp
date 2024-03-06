namespace BuildingBlocks.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class IntegrationEventDecoratorAttribute : Attribute
{
    public string Navigator { get; set; }

    public IntegrationEventDecoratorAttribute(string navigator)
    {
        Navigator = navigator;
    }
}