namespace Users.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);

    Task<string[]> GetUserRolesByUserAsync(User user);

    Task<bool> CheckPasswordAsync(User user, string password);

    Task<bool> UserIsBlockedAsync(User user, string password);

    Task<IdentityResult> CreateUserAsync(User user, string password);

    Task<IdentityResult> UserToRoleAsync(User user, string role);

    Task<IdentityResult> ConfirmUserAsync(User user, string token);

    Task<string> GenerateEmailConfirmationTokenAsync(User user);

    Task<string> GeneratePasswordResetTokenAsync(User user);

    Task<IdentityResult> SetTokenAsync(User user, string loginProvider, string tokenName, string token);

    Task<IdentityResult> RemoveTokenAsync(User user, string loginProvider, string tokenName);

    Task<IdentityResult> ResetUserPasswordAsync(User user, string token, string password);

    Task<User?> GetUserWithRefreshTokensByIdAsync(string userId);

    Task<RefreshToken?> GetRefreshTokenByValueNTAsync(string tokenValue);

    Task<RefreshToken?> GetRefreshTokenByValueAsync(string tokenValue);

    Task SaveAsync();
}