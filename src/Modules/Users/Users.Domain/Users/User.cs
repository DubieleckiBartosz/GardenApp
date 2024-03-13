using Microsoft.AspNetCore.Identity;

namespace Users.Domain.Users;

public class User : IdentityUser, IAggregateRoot
{
    public string FirstName { get; }
    public string LastName { get; }
    public string City { get; }

    private User()
    {
    }

    private User(
        StringValue firstName,
        StringValue lastName,
        StringValue city,
        Phone phoneNumber,
        Email email) : base($"{firstName} {lastName}")
    {
        FirstName = firstName;
        LastName = lastName;
        City = city;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public static User NewUser(
        string firstName,
        string lastName,
        string city,
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

        if (city == null)
        {
            throw new ArgumentNullException(nameof(city));
        }

        return new User(firstName!, lastName!, city!, phoneNumber, email);
    }
}