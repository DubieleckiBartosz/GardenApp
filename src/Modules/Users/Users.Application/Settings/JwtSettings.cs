namespace Users.Application.Settings;

public class JwtSettings
{
    public string? Secret { get; init; }
    public string? Audience { get; init; }
    public string? Issuer { get; init; }
}