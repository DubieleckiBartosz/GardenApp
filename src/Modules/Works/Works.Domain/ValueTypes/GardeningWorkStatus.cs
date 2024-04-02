namespace Works.Domain.ValueTypes;

public class GardeningWorkStatus : Enumeration
{
    public static GardeningWorkStatus OnHold = new GardeningWorkStatus(1, nameof(OnHold));
    public static GardeningWorkStatus InProgress = new GardeningWorkStatus(2, nameof(InProgress));
    public static GardeningWorkStatus CLose = new GardeningWorkStatus(3, nameof(CLose));
    public static GardeningWorkStatus Suspended = new GardeningWorkStatus(4, nameof(Suspended));
    public static GardeningWorkStatus Canceled = new GardeningWorkStatus(5, nameof(Canceled));

    protected GardeningWorkStatus(short id, string name) : base(id, name)
    {
    }
}