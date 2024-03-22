namespace BuildingBlocks.Application.Services;

internal class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    private ClaimsPrincipal? Claims => _httpContextAccessor?.HttpContext?.User;
    private List<Claim>? Roles => Claims?.Claims.Where(_ => _.Type == GardenAppClaimTypes.Role).ToList();

    public bool IsInRole(string roleName)
    {
        var resultRoles = Roles;
        var response = resultRoles?.Any(_ => _.Value == roleName);
        return response ?? false;
    }

    public List<string>? AvailableRoles() => Roles?.Select(_ => _.Value).ToList();

    public bool IsAdmin => IsInRole("Admin");

    public string UserId
    {
        get
        {
            return Claims?.Claims?.FirstOrDefault(_ => _.Type == GardenAppClaimTypes.NameIdentifier)?.Value!;
        }
    }

    public string UserName
    {
        get
        {
            var result = Claims?.Claims.FirstOrDefault(_ => _.Type == GardenAppClaimTypes.UserName)?.Value;
            if (result == null)
            {
                throw new BaseException("User name cannot be null", "User name is null",
                    HttpStatusCode.Unauthorized);
            }

            return result;
        }
    }

    public string Email
    {
        get
        {
            var result = Claims?.Claims.FirstOrDefault(_ => _.Type == GardenAppClaimTypes.Email)?.Value;
            if (result == null)
            {
                throw new BaseException("User mail cannot be null", "User mail is null",
                    HttpStatusCode.Unauthorized);
            }

            return result;
        }
    }
}