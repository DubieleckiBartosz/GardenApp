namespace Users.Domain.Users;

public class RefreshToken
{
    public string Id { get; }
    public string Value { get; }
    public DateTime TokenExpirationDate { get; }
    public bool Revoked { get; private set; }
    public string? ReplacedByToken { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= TokenExpirationDate;
    public bool IsActive => !Revoked && !IsExpired;
    public string UserId { get; }
    public User User { get; }

    private RefreshToken()
    {
    }

    private RefreshToken(TimeSpan duration, string userId)
    {
        Id = Guid.NewGuid().ToString();
        Value = this.GenerateRefreshToken();
        TokenExpirationDate = Clock.CurrentDate().Add(duration);
        Revoked = false;
        UserId = userId;
    }

    internal static RefreshToken CreateNew(TimeSpan duration, string userId) => new(duration, userId);

    public void RevokeToken() => Revoked = true;

    internal void ReplaceToken(string newTokenValue)
    {
        ReplacedByToken = newTokenValue;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}