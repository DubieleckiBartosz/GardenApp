namespace BuildingBlocks.Application.Options;

public class RabbitOptions
{
    public int Port { get; set; }
    public string Host { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string User { get; set; } = default!;
}