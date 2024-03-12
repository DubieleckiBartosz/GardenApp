namespace Users.Infrastructure.Database.Models;

public class ApplicationUser : IdentityUser
{
    private ApplicationUser()
    {
    }

    private ApplicationUser(
        string city,
        string email,
        string phone,
        string firstName,
        string lastName,
        string userName) : base(userName)
    {
        City = city;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phone;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string City { get; }

    public static implicit operator ApplicationUser(User user)
        => new ApplicationUser(user.City, user.Email, user.PhoneNumber, user.FirstName, user.LastName, user.UserName);
}