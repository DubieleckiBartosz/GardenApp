namespace Users.Application.Security;

internal class TokenGenerator
{
    public static string GenerateToken(User user, string[] roles, JwtSettings jwtSettings)
    {
        var roleClaims = new List<Claim>();
        roleClaims.AddRange(roles.Select(role => new Claim(GardenAppClaimTypes.Role, role)).ToList());

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, $"{user.UserName}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        }.Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}