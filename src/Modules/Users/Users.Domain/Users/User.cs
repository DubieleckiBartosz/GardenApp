namespace Users.Domain.Users;

public class User : IdentityUser, IAggregateRoot
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? BusinessId { get; private set; }
    public string? BusinessName { get; private set; }
    public List<RefreshToken> RefreshTokens { get; private set; } = new();

    private User()
    {
    }

    private User(
        StringValue firstName,
        StringValue lastName,
        Phone phoneNumber,
        Email email) : base($"{firstName}_{lastName}")
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        EmailConfirmed = false;
    }

    public static User NewUser(
        string firstName,
        string lastName,
        Phone phoneNumber,
        Email email)
    {
        if (firstName == null)
        {
            throw new ArgumentNullException(nameof(firstName));
        }

        if (lastName == null)
        {
            throw new ArgumentNullException(nameof(lastName));
        }

        return new User(firstName!, lastName!, phoneNumber, email);
    }

    public RefreshToken GenerateNewRefreshToken(TimeSpan duration)
    {
        if (CurrentlyActiveRefreshToken != null)
        {
            throw new SingleActiveTokenException();
        }

        var newRefreshToken = RefreshToken.CreateNew(duration, this.Id);
        RefreshTokens.Add(newRefreshToken);

        return newRefreshToken;
    }

    public RefreshToken NewRefreshToken(string refreshTokenKey)
    {
        var result = RefreshTokens.SingleOrDefault(_ => _.Value == refreshTokenKey);
        if (result == null)
        {
            throw new TokenNotFoundException(refreshTokenKey);
        }

        result.RevokeToken();
        var refreshToken = this.GenerateNewRefreshToken(TimeSpan.FromDays(60));
        result.ReplaceToken(refreshToken.Value);

        return refreshToken;
    }

    public void Confirm()
    {
        EmailConfirmed = true;
    }

    public RefreshToken? CurrentlyActiveRefreshToken => RefreshTokens?.FirstOrDefault(_ => _.IsActive);
}