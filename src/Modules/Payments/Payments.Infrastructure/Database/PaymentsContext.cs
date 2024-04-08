namespace Payments.Infrastructure.Database;

internal class PaymentsContext : DbContext
{
    internal const string PaymentsSchema = "payments";
    private readonly IDomainDecorator _decorator;

    internal DbSet<Payer> Payers { get; set; }
    internal DbSet<PaymentSession> PaymentSessions { get; set; }
    internal DbSet<Subscription> Subscriptions { get; set; }
    internal DbSet<SubPayment> SubPayments { get; set; }
    internal DbSet<Template> Templates { get; set; }

    public PaymentsContext()
    {
    }

    public PaymentsContext(IDomainDecorator decorator, DbContextOptions<PaymentsContext> options) : base(options)
    {
        _decorator = decorator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(PaymentsSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentsContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        await this._decorator.DispatchDomainEventsAsync(this);

        return await SaveChangesAsync(cancellationToken);
    }
}