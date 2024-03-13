namespace Users.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UserToRoleAsync(User user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<User?> GetUserByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<string[]> GetUserRolesByUserAsync(User user)
    {
        return (await _userManager.GetRolesAsync(user)).ToArray();
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<bool> UserIsBlockedAsync(User user, string password)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
        return !result.Succeeded;
    }

    public async Task<bool> UserIsStillAllowedToSignInAsync(User user)
    {
        var result = await _signInManager.CanSignInAsync(user);
        return result;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }

    public async Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }

    public async Task<IdentityResult> ResetUserPasswordAsync(User user, string token, string password)
    {
        var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
        return resetPassResult;
    }

    public async Task<IdentityResult> ConfirmUserAsync(User user, string token)
    {
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result;
    }
}