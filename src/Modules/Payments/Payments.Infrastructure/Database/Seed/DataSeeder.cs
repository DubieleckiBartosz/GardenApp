namespace Payments.Infrastructure.Database.Seed;

internal class DataSeeder
{
    private readonly PaymentsContext _context;

    public DataSeeder(PaymentsContext context)
    {
        _context = context;
    }

    internal async Task SeedTemplatesAsync()
    {
        PaymentTemplateType[] types = { PaymentTemplateType.Success };

        foreach (var templateType in types)
        {
            var type = (int)templateType;
            if (!_context.Templates.Any(_ => _.TemplateType == type))
            {
                var template = Templates.Get(templateType);
                var newTemplate = new Template(type, template.Subject, template.Value);

                await _context.Templates.AddAsync(newTemplate);
            }
        }

        await _context.SaveChangesAsync();
    }
}