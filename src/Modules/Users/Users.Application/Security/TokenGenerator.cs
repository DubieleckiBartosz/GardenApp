using System.Data;

namespace Users.Application.Security;

internal static class TokenGenerator
{
    internal static async Task<(string, RefreshToken)> GenerateAuthorizationTokensAsync(this IUserRepository userRepository, string userId, JwtSettings jwtSettings)
    {
        var user = await userRepository.GetUserWithRefreshTokenAsync(userId) ??
            throw new NotFoundException(StringMessages.UserNotFound);

        var roles = await userRepository.GetUserRolesByUserAsync(user);

        var roleClaims = new List<Claim>();
        roleClaims.AddRange(roles.Select(role => new Claim(GardenAppClaimTypes.Role, role)).ToList());

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, $"{user.UserName}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        }.Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        var refreshToken = user.GenerateNewRefreshToken(TimeSpan.FromDays(60));
        await userRepository.SaveAsync();

        return (token, refreshToken);
    }
}