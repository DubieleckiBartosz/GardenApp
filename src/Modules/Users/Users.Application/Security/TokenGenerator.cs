namespace Users.Application.Security;

internal static class TokenGenerator
{
    internal static string GenerateToken(User user, string[] roles, JwtSettings jwtSettings)
    {
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

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    internal static async Task<RefreshToken> GenerateRefreshToken(this IUserRepository userRepository, string userId)
    {
        var user = await userRepository.GetUserWithRefreshTokenAsync(userId) ??
            throw new NotFoundException(StringMessages.UserNotFound); ;

        var refreshToken = user.GenerateNewRefreshToken(TimeSpan.FromDays(60));
        await userRepository.SaveAsync();
        return refreshToken;
    }
}