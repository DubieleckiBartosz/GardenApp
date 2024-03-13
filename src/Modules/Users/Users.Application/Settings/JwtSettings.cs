namespace Users.Application.Settings;

internal class JwtSettings
{
    public string? Key { get; init; }
    public string? Audience { get; init; }
    public string? Issuer { get; init; }
}