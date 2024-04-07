namespace Payments.Infrastructure.Database.Seed;

internal class ExternalSeeder
{
    private readonly IStripeClient _stripeClient;
    private readonly ILogger _logger;

    public ExternalSeeder(IStripeClient stripeClient, ILogger logger)
    {
        _stripeClient = stripeClient;
        _logger = logger;
    }

    public async Task SeedProducts()
    {
        var productService = new ProductService(_stripeClient);
        if (!await AnyExistingProducts(productService))
        {
            var productCreateOptions = new ProductCreateOptions
            {
                Name = "Base",
                Type = "service", // For SaaS we should use "service"
            };

            var product = await productService.CreateAsync(productCreateOptions);

            var priceService = new PriceService(_stripeClient);
            var priceCreateOptions = new PriceCreateOptions
            {
                UnitAmountDecimal = 16999m,
                Currency = "pln",
                Recurring = new PriceRecurringOptions
                {
                    Interval = "month",
                },
                Product = product.Id,
            };

            var price = await priceService.CreateAsync(priceCreateOptions);
            _logger.Information("STRIPE - Data Seeding Completed");
        }
        else
        {
            _logger.Information("There are existing products registered in this Stripe account.");
        }
    }

    private async Task<bool> AnyExistingProducts(ProductService productService)
    {
        var listOptions = new ProductListOptions
        {
            Active = true,
            Limit = 1
        };

        var existingProducts = await productService.ListAsync(listOptions);
        return existingProducts.Any();
    }
}