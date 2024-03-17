namespace Users.Application.Responses;

public class IdentityErrorResponse
{
    public string Code { get; }
    public string Description { get; }

    internal IdentityErrorResponse(string code, string description)
    {
        Code = code;
        Description = description;
    }
}