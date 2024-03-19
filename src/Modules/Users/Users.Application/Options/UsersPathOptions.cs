namespace Users.Application.Options;

public class UsersPathOptions
{
    public string ClientAddress { get; set; } = default!;
    public string ResetPasswordPath { get; set; } = default!;
    public string ConfirmUserPath { get; set; } = default!;

    public Uri ConfirmRouteUri => new Uri(string.Concat($"{ClientAddress}", ConfirmUserPath));
    public Uri ResetRouteUri => new Uri(string.Concat($"{ClientAddress}", ResetPasswordPath));
}