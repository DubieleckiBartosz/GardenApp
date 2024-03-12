namespace Users.Application.Interfaces;

public interface IUserRepository
{
    Task<IdentityResult> CreateUserAsync(User user, string password);

    Task<IdentityResult> UserToRoleAsync(User user, string role);

    Task<IdentityResult> ConfirmUserAsync(User user, string token);

    Task<string> GenerateEmailConfirmationTokenAsync(User user);

    Task<string> GeneratePasswordResetTokenAsync(User user);

    Task<IdentityResult> ResetUserPasswordAsync(User user, string token, string password);
}